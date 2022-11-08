using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Connection name")]
        public string Name { get; set; }

        [Display(Name = "Connection description")]
        public string Description { get; set; }

        [Display(Name = "CPU type")]
        public CpuType Type { get; set; }

        [Display(Name = "IP address")]
        public string IP_Address { get; set; }
        public short Rack { get; set; }
        public short Slot { get; set; }

        public virtual ICollection<SiemensTag> Tags { get; set; }

        [NotMapped]
        private Plc plc;

        [NotMapped]
        public bool Connected { get; set; } = false;

        public bool CreateDriver()
        {
            plc = new Plc(Type, IP_Address, Rack, Slot);

            if (plc != null)
            {
                Console.WriteLine("Created driver for PLC " + Name + "!");
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
                Console.WriteLine("Connected to PLC " + Name + "!");
            }
            catch (Exception)
            {
                Connected = false;
                return false;
            }
            Connected = true;
            return true;
        }

        public bool Disconnect()
        {
            if (plc != null)
            {
                plc.Close();
                Console.WriteLine("Disconnected from PLC " + Name + "!");

                if (plc.IsConnected == false)
                {
                    Connected = false;
                    return true;
                }
                else
                {
                    Connected = true;
                    return false;
                }
            } else
            {
                Connected = false;
                return true;
            }   
        }

        public bool IsConnected()
        {
            if(plc != null)
            {
                return plc.IsConnected;
            }
            else
            {
                CreateDriver();
                return plc.IsConnected;
            }
            Console.WriteLine("Plc " + Name + " connection is " + plc.IsConnected + "!");

        }

    }
}
