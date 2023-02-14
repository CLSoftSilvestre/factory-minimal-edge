using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;

namespace factory_minimal_edge_ui.Services
{
    public class TestScriptingService : BackgroundService
    {
        public TestScriptingService()
        {

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var engine = new V8ScriptEngine())
                {
                    engine.AddHostType("Console", typeof(Console));
                    engine.AddHostObject("random", new Random());
                    engine.AddHostObject("lib", new HostTypeCollection("mscorlib", "System.Core"));

                    engine.Script.input = 1;
                    engine.Execute(@"Console.WriteLine('Saida do scripting engine ' + input + ' double= ' + random.NextDouble())");
                }

                await Task.Delay(1000);
            }   
        }
    }
}
