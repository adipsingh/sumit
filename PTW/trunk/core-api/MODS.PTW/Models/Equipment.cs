﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MODS.PTW.Models
{
    public class Equipment
    {
        public int ID { get; set; }
        public string Name { get; set; }        
        public int CatagoryId { get; set; }
        [ForeignKey("CatagoryId")]
        public virtual EquipmentCatagory equipmentCatagories { get; set; }
    }
}
