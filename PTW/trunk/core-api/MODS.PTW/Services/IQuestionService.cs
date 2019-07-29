using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface IQuestionService
    {
        Question Create(Question certificate);
        Question Update(Question certificate,int id);
        Question Delete(int id);
        List<Question> GetCertificateQuestion();
        Question GetById(int id);        
    }
}
