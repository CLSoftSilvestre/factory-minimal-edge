using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;

namespace factory_minimal_edge_ui.Services
{
    public class ScriptingService
    {
        public object RunScript(object input, string code)
        {
            object returnValue = null;

            using (var engine = new V8ScriptEngine())
            {
                engine.AddHostType("Console", typeof(Console));
                engine.AddHostObject("random", new Random());
                engine.AddHostObject("lib", new HostTypeCollection("mscorlib", "System.Core"));

                engine.Script.input = input;

                engine.Execute(code);

                returnValue = engine.Script.output;
            }
            return returnValue;
        }
    }
}
