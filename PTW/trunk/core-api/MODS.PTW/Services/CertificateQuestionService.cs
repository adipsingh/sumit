using Microsoft.EntityFrameworkCore;
using MODS.PTW.Helpers;
using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public class CertificateQuestionService : ICertificateQuestionService
    {
        private DataContext _context;
        public CertificateQuestionService(DataContext context)
        {
            _context = context;
        }
        public List<CertificateQuestion> GetCertificateQuestionAnswer(int id)
        {
            var data = (from x in _context.CertificateQuestions
                        where x.CertificateID == id
                        select x).ToList();
            return data;
        }
    }
}
