using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public Certificate Create(Certificate certificate)
        {
            if (string.IsNullOrEmpty(certificate.Name))
            {
                throw new ArgumentException("Certificate Name should not be empty ");
            }
            if (_context.Certificates.Any(x => x.Name == certificate.Name))
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

            _context.Certificates.Add(certificate);
            _context.SaveChanges();

            return certificate;
        }

        public Certificate Delete(int id)
        {
            var data = _context.Certificates.Find(id);
            _context.Certificates.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public List<Certificate> GetAllCertificate()
        {
         
            var data = (from x in _context.Certificates
                        select x).Include(x => x.CertificateQuestions).ToList();
            return data;
        }

        public Certificate GetById(int id)
        {
            return _context.Certificates.Find(id);
        }

        public Certificate Update(Certificate certificate, int id)
        {
            var certi = (from x in _context.Certificates
                         where x.ID == id
                       select x).FirstOrDefault();
            if (_context.Certificates.Any(x => x.Name == certificate.Name))
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
                certi.IsCertificateQA = certificate.IsCertificateQA;
            }

            _context.Certificates.Update(certi);
            _context.SaveChanges();

            return certi;
        }
    }
}
