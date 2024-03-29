﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Dtos;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class EquipmentCatagoryService : IEquipmentCatagoryService
    {
        private DataContext _context;

        public EquipmentCatagoryService(DataContext context)
        {
            _context = context;
        }
        public EquipmentCatagory Create(EquipmentCatagory equipmentCatagory)
        {
            if (string.IsNullOrEmpty(equipmentCatagory.Catagory))
            {
                    throw new ArgumentException("Equipment catagory should not be empty ");
            }
            if (_context.EquipmentCatagory.Any(x => x.Catagory == equipmentCatagory.Catagory))
            {
                throw new AppException("Equipment is already Exists");
            }
                _context.EquipmentCatagory.Add(equipmentCatagory);
                _context.SaveChanges();

                return equipmentCatagory;                       
        }

        public EquipmentCatagory Delete(int id)
        {
            var data=_context.EquipmentCatagory.Find(id);
            _context.EquipmentCatagory.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public List<EquipmentCatagory> GetAllEquipments()
        {
            
            return _context.EquipmentCatagory.ToList();
        }

        public EquipmentCatagory GetById(int id)
        {
           return _context.EquipmentCatagory.Find(id);           
        }        

        public EquipmentCatagory Update(EquipmentCatagory equipment , int id)
        {
            var equip = (from x in _context.EquipmentCatagory 
                        where x.ID==equipment.ID 
                        select x).FirstOrDefault();
            if (string.IsNullOrEmpty(equipment.Catagory))
            {
                throw new ArgumentException("Equipment catagory should not be empty ");
            }
            if (_context.EquipmentCatagory.Any(x => x.Catagory == equipment.Catagory))
            {
                throw new AppException("Equipment is already Exists");
            }
            if (equip.ID == id)
            {
                equip.Catagory = equipment.Catagory;
            }           
            _context.EquipmentCatagory.Update(equip);
            _context.SaveChanges();

            return equip;
        }
    }
}
