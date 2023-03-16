using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;

namespace factory_minimal_edge_ui.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagSource { get; set; }
        public string TagName { get; set; }
        public string TagPath { get { return TagSource + "/" + TagName; } }
        public object TagValue { get; set; }
        public bool CalculatedTag { get; set; }
        public DateTime TagValueDateTime { get; set; }
        public object DataType { get; set; }
        public string Script { get; set; }

        public Tag(string VarName)
        {
            TagName = VarName;
        }

        public Tag(string VarName, object VarValue)
        {
            TagName = VarName;
            TagValue = VarValue;
            TagValueDateTime = DateTime.Now;
        }

        public Tag(string VarSource, string VarName, object VarValue)
        {
            TagSource = VarSource;
            TagName = VarName;
            TagValue = VarValue;
            TagValueDateTime = DateTime.Now;
        }

        public Tag(string VarSource, string VarName, object VarValue, object VarDataType)
        {
            TagSource = VarSource;
            TagName = VarName;
            TagValue = VarValue;
            TagValueDateTime = DateTime.Now;
            DataType = VarDataType;
        }

        public void UpdateTagValue(object newValue)
        {
            if (CalculatedTag)
            {
                Calculate(newValue);
            } else
            {
                TagValue = newValue;
            }
        }

        private void Calculate(object newValue)
        {
            object returnValue = null;

            using (var engine = new V8ScriptEngine())
            {
                engine.AddHostType("Console", typeof(Console));
                engine.AddHostObject("random", new Random());
                engine.AddHostObject("lib", new HostTypeCollection("mscorlib", "System.Core"));

                engine.Script.input = newValue;

                engine.Execute(Script);

                returnValue = engine.Script.output;
            }

            TagValue = returnValue;

        }
    }
}
