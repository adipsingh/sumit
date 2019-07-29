using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class PTWQuestionService : IPTWQuestionService
    {
        private DataContext _context;
        public PTWQuestionService(DataContext context)
        {
            _context = context;
        }
        public PTWQuestion Create(PTWQuestion question)
        {
            if (string.IsNullOrEmpty(question.Questions))
            {
                throw new ArgumentException("Question Name should not be empty ");
            }
            if (_context.PTWSelectionQA.Any(x => x.Questions == question.Questions))
            {
                throw new AppException("Question name is already Exists");
            }
            if(question.CreatedOn==null)
            {
                question.CreatedOn = DateTime.Now;
            }
            if(question.ModifiedOn==null)
            {
                question.ModifiedOn = DateTime.Now;
            }
            _context.PTWSelectionQA.Add(question);
            _context.SaveChanges();

            return question;
        }

        public PTWQuestion Delete(int id)
        {
            var data = _context.PTWSelectionQA.Find(id);
            _context.PTWSelectionQA.Remove(data);
            _context.SaveChanges();
            return data;
        }

            public List<PTWQuestion> GetAllQuestions()
        {
            return _context.PTWSelectionQA.ToList();
        }

        public PTWQuestion GetById(int id)
        {
            return _context.PTWSelectionQA.Find(id);
        }

        public PTWQuestion Update(int id, PTWQuestion question)
        {
            var que = (from x in _context.PTWSelectionQA
                       where x.ID == id
                       select x).FirstOrDefault();
            if (string.IsNullOrEmpty(question.Questions))
            {
                throw new ArgumentException("Question name should not be empty");
            }
            if (que.ID == id)
            {
                que.Questions = question.Questions;
                if (question.CreatedOn == null)
                {
                    que.CreatedOn = DateTime.Now;
                }
                if (question.ModifiedOn == null)
                {
                    que.ModifiedOn = DateTime.Now;
                }
            }

            if (_context.PTWSelectionQA.Any(x => x.Questions == question.Questions))
            {
                throw new AppException("Question name is already Exists");
            }
            _context.PTWSelectionQA.Update(que);
            _context.SaveChanges();

            return que;
        }
    }
}
