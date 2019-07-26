using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class WorkPermitTypesService : IWorkPermitTypesService
    {


        private DataContext _context;
        public WorkPermitTypesService(DataContext context)
        {
            _context = context;
        }


        public WorkPermitTypes Create(WorkPermitTypes workPermitTypes)
        {
            if (string.IsNullOrEmpty(workPermitTypes.PermitType))
            {
                throw new ArgumentException("Permit type  should not be empty ");
            }
            if (_context.WorkPermitTypes.Any(x => x.PermitType == workPermitTypes.PermitType))
            {
                throw new AppException("Permit type is already Exists");
            }
            _context.WorkPermitTypes.Add(workPermitTypes);
            _context.SaveChanges();

            return workPermitTypes;
        }

        public WorkPermitTypes Delete(int id)
        {
            var data = _context.WorkPermitTypes.Find(id);
            _context.WorkPermitTypes.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public List<WorkPermitTypes> GetAllWorkPermitTypes()
        {
            return _context.WorkPermitTypes.ToList();
        }

        public WorkPermitTypes GetById(int id)
        {
            return _context.WorkPermitTypes.Find(id);
        }

        public WorkPermitTypes Update(WorkPermitTypes workPermitTypes, int id)
        {
            var workPermit = (from x in _context.WorkPermitTypes
                              where x.ID == workPermitTypes.ID
                              select x).FirstOrDefault();
            if (_context.WorkPermitTypes.Any(x => x.PermitType == workPermitTypes.PermitType))
            {
                throw new AppException("permit type is already Exists");
            }
            if(string.IsNullOrEmpty(workPermitTypes.PermitType))
            {
                throw new ArgumentException("permit type should not be Empty");
            }
            if (workPermit.ID == id)
            {
                workPermit.PermitType = workPermitTypes.PermitType;

            }

            
            _context.WorkPermitTypes.Update(workPermit);
            _context.SaveChanges();

            return workPermit;
        }
    }
}
