using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Models;

namespace MODS.PTW.Dtos
{
    public class EquipmentCatagoryDto
    {
        public int ID { get; set; }
        public string Catagory { get; set; }
        public ICollection<Equipment> Equipment { get; set; }

    }
}
