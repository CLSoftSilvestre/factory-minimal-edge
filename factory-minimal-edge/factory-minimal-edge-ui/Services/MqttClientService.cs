using factory_minimal_edge_ui.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using System.Text;

namespace factory_minimal_edge_ui.Services
{
    public class MqttClientService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TemporaryDataService _temporaryData;

        public MqttClientService(IServiceScopeFactory scopeFactory, TemporaryDataService temporaryData)
        {
            _scopeFactory = scopeFactory;
            _temporaryData = temporaryData;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var broker = await _context.Brokers.FirstOrDefaultAsync();

                Console.WriteLine("Connecting to " + broker.Name);
                var mqttFactory = new MqttFactory();

                var mqttClient = mqttFactory.CreateMqttClient();
                
                var mqttClientOptions = new MqttClientOptionsBuilder()
                                            .WithClientId(broker.ClientId)
                                            .WithTcpServer(broker.Host, broker.Port)
                                            .WithCleanSession()
                                            .Build();

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                mqttClient.ConnectedAsync += MqttClient_ConnectedAsync;

                mqttClient.DisconnectedAsync += MqttClient_DisconnectedAsync;

                mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;

                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(
                        f =>
                        {
                            f.WithTopic("#");
                            f.WithAtLeastOnceQoS();
                        })
                    .Build();

                Console.WriteLine("MQTT client subscribed to topic.");

                // The response contains additional data sent by the server after subscribing.
                var response = await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

                while (!cancellationToken.IsCancellationRequested)
                {
                        
                }  
                
            }
        }

        private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            Console.WriteLine("Topic: " + arg.ApplicationMessage.Topic + " - Value: " + Encoding.UTF8.GetString(arg.ApplicationMessage.Payload));
            
            // TODO: Update correspondent internal tag.
            return Task.CompletedTask;
        }

        private Task MqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            Console.WriteLine("Disconnected from the MQTT broker. " + arg.ToString());
            return Task.CompletedTask;
        }

        private Task MqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            Console.WriteLine("Connected to MQTT broker. " + arg.ToString());
            return Task.CompletedTask;
        }
    }
}
