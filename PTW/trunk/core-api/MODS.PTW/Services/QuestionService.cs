using Microsoft.EntityFrameworkCore;
using MODS.PTW.Helpers;
using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public class QuestionService:IQuestionService
    {
        private DataContext _context;
        public QuestionService(DataContext context) { _context = context; }

        public Question Create(Question certificate)
        {
            if (string.IsNullOrEmpty(certificate.QuestionTitle))
            {
                throw new ArgumentException("Question Name should not be empty ");
            }
            if (_context.Certificates.Any(x => x.Name == certificate.QuestionTitle))
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

            _context.Questions.Add(certificate);
            _context.SaveChanges();

            return certificate;
        }

        public Question Delete(int id)
        {
            var data = _context.Questions.Find(id);
            _context.Questions.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public Question GetById(int id)
        {
            var data= _context.Questions.Include(p=>p.CertificateQuestions).SingleOrDefault(p=>p.ID==id);
            return data;
        }        

        public List<Question> GetCertificateQuestion()
        {
            var data = (from x in _context.Questions
                        select x).Include(x => x.CertificateQuestions).ToList();
            return data;

        }

        public Question Update(Question certificate, int id)
        {
            var qa = (from x in _context.Questions
                      where x.ID == id
                         select x).FirstOrDefault();

            if (_context.Questions.Any(x => x.QuestionTitle == certificate.QuestionTitle))
            {
                throw new AppException("Questions name is already Exists");
            }
            if (qa.ID == id)
            {
                if (!String.IsNullOrEmpty(certificate.QuestionTitle))
                {
                    qa.QuestionTitle = certificate.QuestionTitle;
                }
                else
                {
                    qa.QuestionTitle = qa.QuestionTitle;
                }                              

                qa.CreatedBy = certificate.CreatedBy;

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

            _context.Questions.Update(qa);
            _context.SaveChanges();

            return qa;
        }
    }
}
