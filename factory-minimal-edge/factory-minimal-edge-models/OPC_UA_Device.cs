using System;
using System.ComponentModel.DataAnnotations;
using Opc.Ua.Client;

namespace factory_minimal_edge_models
{
    public class OPC_UA_Device
    {
        public int Id { get; set; }

        [Display(Name = "Connection name")]
        public string Name { get; set; }

        [Display(Name = "Endpoint")]
        public string Endpoint { get; set; }

        // Need to add more variables regarding connection
    }
}
