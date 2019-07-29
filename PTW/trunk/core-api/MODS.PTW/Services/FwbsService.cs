using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class FwbsService : IFwbsService
    {
        private DataContext _context;
        public FwbsService(DataContext context) { _context = context; }
        public Fwbs Create(Fwbs fwbs)
        {
            if (string.IsNullOrEmpty(fwbs.Name))
            {
                throw new ArgumentException("FWBS Name should not be empty ");
            }
            if (_context.FWBS.Any(x => x.Name == fwbs.Name) )
            {
                throw new AppException("FWBS name is already Exists");
            }
            //if(fwbs.MediaID==0)
            //{
            //    throw new AppException("Media Is not Cretaed ");
            //}
            if (fwbs.CreatedOn == null)
            {
                fwbs.CreatedOn = DateTime.Now;
            }
            if (fwbs.ModifiedOn == null)
            {
                fwbs.ModifiedOn = DateTime.Now;
            }
            _context.FWBS.Add(fwbs);
            _context.SaveChanges();

            return fwbs;
        }

        public List<Fwbs> GetAllFwbs()
        {
            return _context.FWBS.ToList();
        }

        public Fwbs GetById(int id)
        {
            return _context.FWBS.Find(id);
        }

        public Fwbs GetByName(string name)
        {
            var fwbsData = (from f in _context.FWBS
                            where f.Name == name
                            select f).FirstOrDefault();
            return fwbsData;
        }

        public Fwbs Save(Fwbs fwbs)
        {
            var fwbsData = (from f in _context.FWBS
                            where f.Name == fwbs.Name
                            select f).FirstOrDefault();

            if (fwbsData.ID == fwbs.ID)
            {
                if (!string.IsNullOrEmpty(fwbs.Name))
                {
                    fwbsData.Name = fwbs.Name;
                }
                if (fwbs.MediaID == 0)
                {
                    throw new AppException("Media Is not Cretaed ");
                }
                else
                {
                    fwbsData.MediaID = fwbs.MediaID;
                }
                if (fwbs.CreatedOn == null)
                {
                    fwbs.CreatedOn = DateTime.Now;
                }
                if (fwbs.ModifiedOn == null)
                {
                    fwbs.ModifiedOn = DateTime.Now;
                }
            }
            _context.FWBS.Update(fwbs);
            _context.SaveChanges();

            return fwbs;
        }
    }
}
