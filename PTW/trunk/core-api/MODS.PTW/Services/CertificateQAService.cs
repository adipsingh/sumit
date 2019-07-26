using MODS.PTW.Helpers;
using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public class CertificateQAService:ICertificateQAService
    {
        private DataContext _context;
        public CertificateQAService(DataContext context) { _context = context; }

        public CertificateQA create(CertificateQA certificate)
        {
            if (string.IsNullOrEmpty(certificate.Questions))
            {
                throw new ArgumentException("Question Name should not be empty ");
            }
            if (_context.Certificate.Any(x => x.Name == certificate.Questions))
            {
                throw new AppException("Question name is already Exists");
            }

            if (certificate.CreatedOn == null)
            {
                certificate.CreatedOn = DateTime.Now;
            }
            if (certificate.ModifiedOn == null)
            {
                certificate.ModifiedOn = DateTime.Now;
            }

            _context.CertificateQA.Add(certificate);
            _context.SaveChanges();

            return certificate;
        }

        public CertificateQA delete(int id)
        {
            var data = _context.CertificateQA.Find(id);
            _context.CertificateQA.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public CertificateQA GetById(int id)
        {
            return _context.CertificateQA.Find(id);
        }

        public List<CertificateQA> GetCertificateQA()
        {
            return _context.CertificateQA.ToList();
        }

        public CertificateQA update(CertificateQA certificate, int id)
        {
            var qa = (from x in _context.CertificateQA
                         where x.ID == id
                         select x).FirstOrDefault();
            if (_context.CertificateQA.Any(x => x.Questions == certificate.Questions))
            {
                throw new AppException("Questions name is already Exists");
            }
            if (qa.ID == id)
            {
                if (!String.IsNullOrEmpty(certificate.Questions))
                {
                    qa.Questions = certificate.Questions;
                }
                else
                {
                    qa.Questions = qa.Questions;
                }

                qa.IsExcavation = certificate.IsExcavation;
                qa.IsConfinedSpace = certificate.IsConfinedSpace;
                qa.IsRadioGraphy = certificate.IsRadioGraphy;
                qa.IsEnergization = certificate.IsEnergization;
                qa.IsElectrical = certificate.IsElectrical;
                qa.IsCriticalLift = certificate.IsCriticalLift;
                qa.IsGratingRemoval = certificate.IsGratingRemoval;                

                qa.createdBy = certificate.createdBy;

                if (certificate.CreatedOn == null)
                {
                    qa.CreatedOn = DateTime.Now;
                }

                qa.ModifiedBy = certificate.ModifiedBy;

                if (certificate.ModifiedOn == null)
                {
                  qa.ModifiedOn = DateTime.Now;
                }                
            }

            _context.CertificateQA.Update(qa);
            _context.SaveChanges();

            return qa;
        }
    }
}
