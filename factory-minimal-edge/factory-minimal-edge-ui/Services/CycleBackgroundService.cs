using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using factory_minimal_edge_ui.Data;
using Microsoft.EntityFrameworkCore;
using S7.Net;
using Microsoft.AspNetCore.SignalR.Client;

namespace factory_minimal_edge_ui.Services
{

    public class CycleBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        HubConnection connection;

        public CycleBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44313/tagsHub")
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await connection.StartAsync();

            Console.WriteLine($"SignalR connection status: {connection.State}");

            using(var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var connections = await _context.SiemensDevices.Include(a => a.Tags).ToListAsync();

                while (!cancellationToken.IsCancellationRequested)
                {
                    foreach (var con in connections)
                    {
                        using (var plc = new Plc(con.Type, con.IP_Address, con.Rack, con.Slot))
                        {
                            plc.Open();

                            if (plc.IsConnected)
                            {
                                foreach (var v in con.Tags)
                                {
                                    v.Value = plc.Read(v.Address);
                                    Console.WriteLine($"{DateTime.Now} | PLC -> {con.Name} : Tag -> {v.Name} : Value - {v.Value}");

                                    try
                                    {
                                        await connection.InvokeAsync("UpdatedTagValue",v.Id.ToString() ,v.Name, v.Value.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                            }
                        }
                    }
                    await Task.Delay(5000);
                }
            }
        }   
    }
}
