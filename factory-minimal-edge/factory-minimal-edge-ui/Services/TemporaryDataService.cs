using System;
using System.Collections.Generic;
using System.Linq;

namespace factory_minimal_edge_ui.Services
{
    public class TemporaryDataVariable
    {
        public string VariableName { get; set; }
        public object VariableValue { get; set; }

        public TemporaryDataVariable(string VarName)
        {
            VariableName = VarName;
        }

        public TemporaryDataVariable(string VarName, object VarValue)
        {
            VariableName = VarName;
            VariableValue = VarValue;
        }
    }

    public class TemporaryDataService
    {
        private List<TemporaryDataVariable> Variables;

        public TemporaryDataService()
        {
            Variables = new List<TemporaryDataVariable>();
        }

        public void AddVariable(string VarName)
        {
            TemporaryDataVariable _tempVar = new TemporaryDataVariable(VarName);
            Variables.Add(_tempVar);
        }

        public void AddVariable(string VarName, object VarValue)
        {
            TemporaryDataVariable _tempVar = new TemporaryDataVariable(VarName, VarValue);
            Variables.Add(_tempVar);
        }

        public void UpdateVariableValue(string VarName, object VarValue)
        {
            TemporaryDataVariable tmpVar = Variables.Where(i => i.VariableName == VarName).FirstOrDefault();

            if (tmpVar == null)
            {
                AddVariable(VarName, VarValue);
            } else
            {
                tmpVar.VariableValue = VarValue;
            }
        }

        public object GetVariableValue(string VarName)
        {
            TemporaryDataVariable tmpVar = Variables.Where(i => i.VariableName == VarName).FirstOrDefault();
            return tmpVar.VariableValue;
        }

    }
}
