using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class SectionService : ISectionService
    {


        private DataContext _context;
        public SectionService(DataContext context)
        {
            _context = context;
        }

        public Section Create(Section section)
        {
            if (string.IsNullOrEmpty(section.Name))
            {
                throw new ArgumentException("section Name should not be empty ");
            }
            if (_context.Functions.Any(x => x.Name == section.Name))
            {
                throw new AppException("section name is already Exists");
            }
            _context.Sections.Add(section);
            _context.SaveChanges();

            return section;
        }

        public Section Delete(int id)
        {

            var data = _context.Sections.Find(id);
            _context.Sections.Remove(data);
            _context.SaveChanges();
            return data; 
        }

        public List<Section> GetAllSection()
        {
            return _context.Sections.ToList();
        }

        public Section GetById(int id)
        {
            return _context.Sections.Find(id);
        }

        public Section Update(Section section, int id)
        {
            var sec = (from x in _context.Sections
                       where x.ID == section.ID
                       select x).FirstOrDefault();
            if(string.IsNullOrEmpty(section.Name))
            {
                throw new ArgumentException("Section name should not be empty");
            }
            if (sec.ID == id)
            {
                sec.Name = section.Name;
            }

            if (_context.Sections.Any(x => x.Name == section.Name))
            {
                throw new AppException("Section name is already Exists");
            }
            _context.Sections.Update(sec);
            _context.SaveChanges();

            return sec;
        }
    }
}
