using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace factory_minimal_edge_ui.Models
{
    public class Tag
    {
        public string TagSource { get; set; }
        public string TagName { get; set; }
        public string TagPath { get { return TagSource + "/" + TagName; } }
        public object TagValue { get; set; }
        public bool CalculatedTag { get; set; }
        public DateTime TagValueDateTime { get; set; }
        public object DataType { get; set; }
        public string CalculationExpression { get; set; }

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
            // Code to parse the calculation Expressions and get back the new value to the variable
            TagValue = newValue;
        }
    }
}
