using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S7.Net;

namespace factory_minimal_edge_models
{
    public class SiemensTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DeviceId { get; set; }
        public virtual SiemensDevice Device { get; set; }
        public VarType Type { get; set; }
        public string Address { get; set; }

        [NotMapped]
        public object Value {get; set;}

        public async Task<bool> UpdateTagValue()
        {
            var tagValue = await Device.GetDriver().ReadAsync(Address);
            Value = tagValue;

            if(tagValue != null)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }

}
