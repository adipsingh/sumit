using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class AreaService : IAreaService
    {

        private DataContext _context;
        public AreaService(DataContext context)
        {
            _context = context;
        }
        public Area Create(Area area)
        {
            if (string.IsNullOrEmpty(area.Name))
            {
                throw new ArgumentException("Area Name should not be empty ");
            }
            if (_context.Areas.Any(x => x.Name == area.Name))
            {
                throw new AppException("Area name is already Exists");
            }
            _context.Areas.Add(area);
            _context.SaveChanges();
            return area;
        }

        public Area Delete(int id)
        {
            var data = _context.Areas.Find(id);
            _context.Areas.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public List<Area> GetAllArea()
        {
            return _context.Areas.ToList();
        }

        public Area GetById(int id)
        {
            return _context.Areas.Find(id);
        }

        public Area Update(Area area, int id)
        {
            var areaData = (from x in _context.Areas
                            where x.ID == area.ID
                       select x).FirstOrDefault();

            if (string.IsNullOrEmpty(area.Name))
            {
                throw new ArgumentException("Area name should not be Empty ");
            }
            if (_context.Areas.Any(x => x.Name == area.Name))
            {
                throw new AppException("Area name is already Exists");
            }
            if (areaData.ID == id)
            {
                areaData.Name = area.Name;
            }            
            _context.Areas.Update(areaData);
            _context.SaveChanges();

            return areaData;
        }
    }  
}
