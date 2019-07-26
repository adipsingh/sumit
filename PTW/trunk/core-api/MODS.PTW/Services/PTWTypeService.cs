using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class PTWTypeService : IPTWTypeService
    {
        private DataContext _context;
        public PTWTypeService(DataContext context)
        {
            _context = context;
        }
        public PTWType Create(PTWType type)
        {
            if (string.IsNullOrEmpty(type.Type))
            {
                throw new ArgumentException("Type Name should not be empty ");
            }
            if (_context.PTWType.Any(x => x.Type == type.Type))
            {
                throw new AppException("Type name is already Exists");
            }
            _context.PTWType.Add(type);
            _context.SaveChanges();

            return type;
        }

        public PTWType Delete(int id)
        {
            var data = _context.PTWType.Find(id);
            _context.PTWType.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public List<PTWType> GetAllTypes()
        {
            return _context.PTWType.ToList();
        }

        public PTWType GetById(int id)
        {
            return _context.PTWType.Find(id);
        }

        public PTWType Update(int id, PTWType type)
        {
            var ptw = (from x in _context.PTWType
                       where x.ID == id
                       select x).FirstOrDefault();
            if (string.IsNullOrEmpty(type.Type))
            {
                throw new ArgumentException("Type should not be empty");
            }
            if (ptw.ID == id)
            {
                ptw.Type = type.Type;
            }

            if (_context.PTWType.Any(x => x.Type == type.Type))
            {
                throw new AppException("Type is already Exists");
            }
            _context.PTWType.Update(ptw);
            _context.SaveChanges();

            return ptw;
        }
    }
}
