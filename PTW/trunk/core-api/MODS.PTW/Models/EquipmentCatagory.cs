using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Models
{
    public class EquipmentCatagory
    {
        [Key]
        public int ID { get; set; }
        public string Catagory { get; set; }
        public ICollection<Equipment> Equipment { get; set; }
    }
}
