using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class CertificateService : ICertificateService
    {
        private DataContext _context;
        public CertificateService(DataContext context)
        {
            _context = context;
        }
        public Certificate create(Certificate certificate)
        {
            if (string.IsNullOrEmpty(certificate.Name))
            {
                throw new ArgumentException("Certificate Name should not be empty ");
            }
            if (_context.Certificate.Any(x => x.Name == certificate.Name))
            {
                throw new AppException("Certificate name is already Exists");
            }

            if (certificate.CreatedOn == null)
            {
                certificate.CreatedOn = DateTime.Now;
            }
            if (certificate.ModifiedOn == null)
            {
                certificate.ModifiedOn = DateTime.Now;
            }

            _context.Certificate.Add(certificate);
            _context.SaveChanges();

            return certificate;
        }

        public Certificate Delete(int id)
        {
            var data = _context.Certificate.Find(id);
            _context.Certificate.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public List<Certificate> GetAllCertificate()
        {
            return _context.Certificate.ToList();
        }

        public Certificate GetById(int id)
        {
            return _context.Certificate.Find(id);
        }

        public Certificate update(Certificate certificate, int id)
        {
            var certi = (from x in _context.Certificate
                         where x.ID == id
                       select x).FirstOrDefault();
            if (_context.Certificate.Any(x => x.Name == certificate.Name))
            {
                throw new AppException("Certificate name is already Exists");
            }
            if (certi.ID == id)
            {
                if (!String.IsNullOrEmpty(certificate.Name))
                {
                    certi.Name = certificate.Name;
                }
                else
                {
                    certi.Name = certi.Name;
                }

                if (certificate.ParentID!=0)
                {
                    certi.ParentID = certificate.ParentID;
                }
                else
                {
                    certi.ParentID = certi.ParentID;
                }

                certi.CreatedBy = certificate.CreatedBy;

                if (certificate.CreatedOn == null)
                {
                    certi.CreatedOn = DateTime.Now;
                }

                certi.ModifiedBy = certificate.ModifiedBy;

                if (certificate.ModifiedOn == null)
                {
                    certi.ModifiedOn = DateTime.Now;
                }
                
                certi.Status = certificate.Status;                
            }

            _context.Certificate.Update(certi);
            _context.SaveChanges();

            return certi;
        }
    }
}
