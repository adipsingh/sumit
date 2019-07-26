using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class QuestionnarieService : IQuestionnarieService
    {

        private DataContext _context;

        public QuestionnarieService(DataContext context)
        {
            _context = context;
        }
        public Questionnarie Create(Questionnarie questionnarie)
        {
            if(string.IsNullOrEmpty(questionnarie.QName))
            {
                throw new ArgumentException("Question name should not be empty ");
            }

            if(_context.questionnaries.Any(x=>x.QName== questionnarie.QName))
            {
                throw new AppException("Question is already Exists");
            }
            _context.questionnaries.Add(questionnarie);
            _context.SaveChanges();

            return questionnarie;
        }
    }
}
