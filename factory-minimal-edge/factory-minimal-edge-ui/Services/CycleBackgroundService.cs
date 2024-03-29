﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using factory_minimal_edge_ui.Data;
using Microsoft.EntityFrameworkCore;
using S7.Net;
using Microsoft.AspNetCore.SignalR.Client;
using System.Linq;

namespace factory_minimal_edge_ui.Services
{

    public class CycleBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TemporaryDataService _temporaryData;
        HubConnection connection;

        public CycleBackgroundService(IServiceScopeFactory scopeFactory, TemporaryDataService temporaryData)
        {
            _scopeFactory = scopeFactory;
            _temporaryData = temporaryData;

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
                var connections = await _context.SiemensDevices.Where(e => e.Active == true).Include(a => a.Tags).ToListAsync();

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
                                    //Console.WriteLine($"{DateTime.Now} | PLC -> {con.Name} : Tag -> {v.Name} : Value - {v.Value}");

                                    // Save data into temporary data
                                    _temporaryData.UpdateVariableValue(con.Name, v.Name, ConvertPLCData(v.Type, v.Value), v.Type);

                                    try
                                    {
                                        // Send data to client via SignalR
                                        await connection.InvokeAsync("UpdatedTagValue",v.Id.ToString() ,v.Name, ConvertPLCData(v.Type, v.Value));
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                            }
                        }
                    }
                    await Task.Delay(1000);
                }
            }
        } 
        
        private string ConvertPLCData(VarType type, Object value)
        {
            switch (type)
            {
                case VarType.Bit:
                    return value.ToString();
                case VarType.Int:
                    return value.ToString();
                case VarType.Real:
                    var dword = (uint)value;
                    float result = dword.ConvertToFloat();
                    return result.ToString();
                default:
                    return value.ToString();
            }
        }
    }
}
