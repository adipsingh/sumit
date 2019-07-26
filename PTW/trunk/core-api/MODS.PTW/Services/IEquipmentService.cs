using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface IEquipmentService
    {
        Equipment Create(Equipment equipment);
        List<Equipment> GetAllEquipments();
        Equipment GetById(int id);
        Equipment Update(Equipment equipment, int id);
        Equipment Delete(int id);
    }
}
