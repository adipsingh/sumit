using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MODS.PTW.Services
{
    public class CertificateAuthorityService : ICertificateAuthorityService
    {
        private DataContext _context;
        public CertificateAuthorityService(DataContext context) { _context = context; }

        public CertificateAuthority create(CertificateAuthority authority)
        {        
            
            if(authority.CreatedOn==null )
            {
                authority.CreatedOn = DateTime.Now;
            }
            if(authority.ModifiedOn==null)
            {
                authority.ModifiedOn = DateTime.Now;
            }
            
            _context.CertificateAuthority.Add(authority);
            _context.SaveChanges();

            return authority;
        }

        public CertificateAuthority Delete(int id)
        {
            var data = _context.CertificateAuthority.Find(id);
            _context.CertificateAuthority.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public List<CertificateAuthority> GetAllCertificateAuthority()
        {
            return _context.CertificateAuthority.ToList();
        }

        public CertificateAuthority GetById(int id)
        {
            return _context.CertificateAuthority.Find(id);
        }

        public CertificateAuthority update(CertificateAuthority authority, int id)
        {
            var certi = (from x in _context.CertificateAuthority
                         where x.ID == id
                         select x).FirstOrDefault();
            
            if (certi.ID == id)
            {
                certi.CertificateID = authority.CertificateID;
                certi.CompanyID = authority.CompanyID;
                certi.SectionID = authority.SectionID;
                certi.FunctionID = authority.FunctionID;
                certi.EmployeeID = authority.EmployeeID;
                certi.CreatedBy = authority.CreatedBy;

                if (authority.CreatedOn == null)
                {
                    certi.CreatedOn = DateTime.Now;
                }

                certi.ModifiedBy = authority.ModifiedBy;

                if (authority.ModifiedOn == null)
                {
                    certi.ModifiedOn = DateTime.Now;
                }

                certi.Status = authority.Status;
            }

            _context.CertificateAuthority.Update(certi);
            _context.SaveChanges();
            return certi;
        }
    }
}
