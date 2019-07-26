using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class EquipmentService : IEquipmentService
    {
        private DataContext _context;

        public EquipmentService(DataContext context)
        {
            _context = context;
        }
        public Equipment Create(Equipment equipment)
        {
            if (string.IsNullOrEmpty(equipment.Name))
            {
                throw new ArgumentException("Equipment Name should not be empty ");
            }
            if (_context.Equipments.Any(x => x.Name == equipment.Name))
            {
                throw new AppException("Equipment is already Exists");
            }
            _context.Equipments.Add(equipment);
            _context.SaveChanges();

            return equipment;
        }

        public Equipment Delete(int id)
        {
            var data = _context.Equipments.Find(id);
            _context.Equipments.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public List<Equipment> GetAllEquipments()
        {
        //    List<string> cat = null;
        //    List<Equipment> data = null;
        //    EquipmentCatagory ec = new EquipmentCatagory();
        //    var equipCatData = (from equip in _context.EquipmentCatagory
        //                        select equip.Catagory).ToList();
        //    var equipment = (from eq in _context.Equipments
        //                     select eq.CatagoryId).ToList();
        //    for (int i = 1; i < equipCatData.Count; i++)
        //    {
               
        //        Equipment equi = new Equipment()
        //        {
        //             if (equi.CatagoryId == ec.ID)
        //        {
        //            cat.Add(equipCatData.ToString());
        //        }
        //    };
        //    }
        //    data.Add(equi.ID);
        //    data.Add(equi.Name);
        //    data.Add(equi.CatagoryId.ToString());
        //    data.Add(cat);
        //}
            return _context.Equipments.ToList();
        }

    public Equipment GetById(int id)
    {
        return _context.Equipments.Find(id);
    }

    public Equipment Update(Equipment equipment, int id)
    {
        var equip = (from x in _context.Equipments
                     where x.ID == id
                     select x).FirstOrDefault();
        if (_context.Equipments.Any(x => x.Name == equipment.Name))
        {
            throw new AppException("Equipment is already Exists");
        }
        if (string.IsNullOrEmpty(equipment.Name))
        {
            throw new ArgumentException("Equipment Name Should not be empty");
        }
        if (equip.ID == id)
        {
            equip.Name = equipment.Name;
        }
        _context.Equipments.Update(equip);
        _context.SaveChanges();

        return equip;
    }
}
}
