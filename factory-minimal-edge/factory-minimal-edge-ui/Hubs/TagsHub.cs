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

        public async Task UpdatedTagValue(string tagId, string tagName, string tagValue)
        {
            await Clients.All.SendAsync("UpdatedTagValue", tagId, tagName, tagValue);
        }
    }
}
