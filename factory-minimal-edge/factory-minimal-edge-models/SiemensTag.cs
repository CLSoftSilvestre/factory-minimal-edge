﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Tag name")]
        public string Name { get; set; }
        [Display(Name = "Controller")]
        public int DeviceId { get; set; }
        [Display(Name = "Controller")]
        public virtual SiemensDevice Device { get; set; }

        [Display(Name = "Variable type")]
        public VarType Type { get; set; }
        public string Address { get; set; }

        [NotMapped]
        public object Value {get; set;}

    }

}
