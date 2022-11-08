using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using factory_minimal_edge_models;
using S7.Net;

namespace factory_minimal_edge_ui.Hubs
{
    public class TagsHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceivedMessage", user, message);
        }

        public async IAsyncEnumerable<SiemensTag> Streaming(CancellationToken cancellationToken)
        {
            bool varm07 = false;

            // Read variable of PLC and send via SinalR
            using (var plc = new Plc(CpuType.S71200, "169.254.190.100", 0, 0))
            {
                //Open connection with PLC
                plc.Open();

                while (true)
                {
                    if (plc.IsConnected)
                    {
                        
                        varm07 = (bool)plc.Read("M0.7");

                        // Create Siemens Tag
                        SiemensTag tag = new SiemensTag();
                        tag.Name = "Tag de Teste";
                        tag.Type = VarType.Bit;
                        tag.Value = varm07;

                        await Clients.All.SendAsync("UpdatedTag", tag);

                        Console.WriteLine(tag.Value);

                        yield return tag;
                        await Task.Delay(1000, cancellationToken);
                    }
                }     
            }

        }
    }
}
