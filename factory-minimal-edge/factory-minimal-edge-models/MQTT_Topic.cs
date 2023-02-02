using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factory_minimal_edge_models
{
    public class MQTT_Topic
    {
        public int Id { get; set; }
        public int BrokerId { get; set; }
        public virtual MQTT_Broker Broker { get; set; }
        public string Topic { get; set; }
        public int QoS { get; set; }

        [NotMapped]
        public object Value { get; set; }

    }
}
