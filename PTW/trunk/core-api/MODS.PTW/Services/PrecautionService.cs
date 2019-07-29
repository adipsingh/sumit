using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class PrecautionService : IPrecautionService
    {
        private DataContext _context;
        public PrecautionService(DataContext context) { _context = context; }
        public PrecautionQA Create(PrecautionQA question)
        {
            if (string.IsNullOrEmpty(question.Question))
            {
                throw new ArgumentException("Question Name should not be empty ");
            }
            if (_context.PrecautionQA.Any(x => x.Question == question.Question))
            {
                throw new AppException("Question name is already Exists");
            }
            if (question.CreatedOn == null)
            {
                question.CreatedOn = DateTime.Now;
            }
            if (question.ModifiedOn == null)
            {
                question.ModifiedOn = DateTime.Now;
            }
            _context.PrecautionQA.Add(question);
            _context.SaveChanges();

            return question;
        }

        public PrecautionQA Delete(int id)
        {
            var data = _context.PrecautionQA.Find(id);
            _context.PrecautionQA.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public List<PrecautionQA> GetAllQuestions()
        {
            return _context.PrecautionQA.ToList();
        }

        public PrecautionQA GetById(int id)
        {
            return _context.PrecautionQA.Find(id);
        }

        public PrecautionQA GetByTypeId(int id)
        {
            var typeID = (from x in _context.PrecautionQA
                       where x.PrecautionTypeID == id
                       select x).FirstOrDefault();
            if (typeID.PrecautionTypeID == id)
            {
                return _context.PrecautionQA.Find(typeID.PrecautionTypeID);
            }
            else
            {
                throw new AppException("Precaution Question Type Id is Not Exists/ Invalid");
            }
        }

        public PrecautionQA Update(int id, PrecautionQA question)
        {
            var que = (from x in _context.PrecautionQA
                       where x.ID == id
                       select x).FirstOrDefault();
            if (string.IsNullOrEmpty(question.Question))
            {
                throw new ArgumentException("Question name should not be empty");
            }
            if (que.ID == id)
            {
                que.Question = question.Question;
                if (question.CreatedOn == null)
                {
                    que.CreatedOn = DateTime.Now;
                }
                if (question.ModifiedOn == null)
                {
                    que.ModifiedOn = DateTime.Now;
                }
                if(question.PrecautionTypeID!=0)
                {
                    que.PrecautionTypeID = question.PrecautionTypeID;
                }
            }

            if (_context.PrecautionQA.Any(x => x.Question == question.Question))
            {
                throw new AppException("Question name is already Exists");
            }
            _context.PrecautionQA.Update(que);
            _context.SaveChanges();

            return que;
        }
    }
}
