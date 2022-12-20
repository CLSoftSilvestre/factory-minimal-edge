using System;
using System.Collections.Generic;
using System.Linq;
using factory_minimal_edge_ui.Models;

namespace factory_minimal_edge_ui.Services
{
    public class TemporaryDataService
    {
        private List<Tag> Variables;

        public TemporaryDataService()
        {
            Variables = new List<Tag>();
        }

        public void AddVariable(string VarName)
        {
            Tag _tempVar = new Tag(VarName);
            Variables.Add(_tempVar);
        }

        public void AddVariable(string VarName, object VarValue)
        {
            Tag _tempVar = new Tag(VarName, VarValue);
            Variables.Add(_tempVar);
        }
        public void AddVariable(string VarSource, string VarName, object VarValue)
        {
            Tag _tempVar = new Tag(VarSource, VarName, VarValue);
            Variables.Add(_tempVar);
        }

        public void AddVariable(string VarSource, string VarName, object VarValue, object VarDataType)
        {
            Tag _tempVar = new Tag(VarSource, VarName, VarValue, VarDataType);
            Variables.Add(_tempVar);
        }

        public void UpdateVariableValue(string VarSource, string VarName, object VarValue, object VarDataType)
        {
            Tag tmpVar = Variables.Where(i => i.TagName == VarName).FirstOrDefault();

            if (tmpVar == null)
            {
                AddVariable(VarSource, VarName, VarValue, VarDataType);
            } else
            {
                tmpVar.UpdateTagValue(VarValue);
                tmpVar.TagValueDateTime = DateTime.Now;
            }
        }

        public object GetVariableValue(string VarName)
        {
            Tag tmpVar = Variables.Where(i => i.TagName == VarName).FirstOrDefault();
            return tmpVar.TagValue;
        }

        public List<Tag> GetVariablesList()
        {
            return Variables;
        }

    }
}
