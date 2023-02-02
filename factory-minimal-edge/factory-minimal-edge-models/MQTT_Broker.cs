using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factory_minimal_edge_models
{
    public class MQTT_Broker
    {
        public int Id { get; set; }

        [Display(Name = "Connection name")]
        public string Name { get; set; }

        public string Protocol { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool ValidateCertificate { get; set; }

        public bool TLSEncryption { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ClientId { get; set; }

        public virtual ICollection<MQTT_Topic> Topics { get; set; }

    }
}
