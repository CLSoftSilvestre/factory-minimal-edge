using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S7.Net;

namespace factory_minimal_edge_models
{
    public class SiemensDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CpuType Type { get; set; }
        public string IP_Address { get; set; }
        public short Rack { get; set; }
        public short Slot { get; set; }

        public virtual ICollection<SiemensTag> Tags { get; set; }

        [NotMapped]
        private Plc plc;

        public bool CreateDriver()
        {
            plc = new Plc(Type, IP_Address, Rack, Slot);

            if (plc != null)
            {
                return true;
            }
            return false;
        }

        public Plc GetDriver()
        {
            return plc;
        }

        public bool Connect()
        {
            if (plc == null)
            {
                CreateDriver();
            }
  
            try
            {
                plc.Open();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Disconnect()
        {
            plc.Close();

            if (plc.IsConnected == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsConnected()
        {
            return plc.IsConnected;
        }

    }
}
