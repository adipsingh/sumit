using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{

    
    
    public class WorkPermitStatusService : IWorkPermitStatusService
    {


        private DataContext _context;

        public WorkPermitStatusService(DataContext context)
        {
            _context = context;
        }



        public WorkPermitStatus Create(WorkPermitStatus workPermitStatus)
        {
            if (string.IsNullOrEmpty(workPermitStatus.Status))
            {
                throw new ArgumentException("Equipment Name should not be empty ");
            }
            if (_context.WorkPermitStatus.Any(x => x.Status == workPermitStatus.Status))
            {
                throw new AppException("Equipment is already Exists");
            }
            _context.WorkPermitStatus.Add(workPermitStatus);
            _context.SaveChanges();

            return workPermitStatus;           
        }

        public WorkPermitStatus Delete(int id)
        {
            var data = _context.WorkPermitStatus.Find(id);
            _context.WorkPermitStatus.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public List<WorkPermitStatus> GetAllWorkPermitStatus()
        {
            return _context.WorkPermitStatus.ToList();
        }

        public WorkPermitStatus GetById(int id)
        {
            return _context.WorkPermitStatus.Find(id);
        }

        public WorkPermitStatus Update(WorkPermitStatus workPermitStatus, int id)
        {
            var workPermit = (from x in _context.WorkPermitStatus
                         where x.ID == workPermitStatus.ID
                         select x).FirstOrDefault();
            if (_context.WorkPermitStatus.Any(x => x.Status == workPermitStatus.Status))
            {
                throw new AppException("Status is already Exists");
            }
            if(string.IsNullOrEmpty(workPermitStatus.Status))
            {
                throw new ArgumentException("Status should not be Empty");
            }
            if (workPermit.ID == id)
            {
                workPermit.Status = workPermitStatus.Status;
            }           
            _context.WorkPermitStatus.Update(workPermit);
            _context.SaveChanges();

            return workPermit;
        }
    }
}
