using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class PTWQuestionTypeAnsService : IPTWQuestiontypeAnsService
    {
        private DataContext _context;
        public PTWQuestionTypeAnsService(DataContext context)
        {
            _context = context;
        }
        public PTWQuestionTypeAns create(PTWQuestionTypeAns pTWQuestionAns)
        {
            _context.PTWQuestionTypeAns.Add(pTWQuestionAns);
            _context.SaveChanges();

            return pTWQuestionAns;
        }

        public PTWQuestionTypeAns update(int id, PTWQuestionTypeAns pTWQuestionAns)
        {
            var ans = (from x in _context.PTWQuestionTypeAns
                       where x.ID == id
                         select x).FirstOrDefault();
            
            if (ans.ID == id)
            {
                ans.QAID = pTWQuestionAns.QAID;
                ans.TypeID = pTWQuestionAns.TypeID;
                ans.Answer = pTWQuestionAns.Answer;
            }
            _context.PTWQuestionTypeAns.Update(ans);
            _context.SaveChanges();

            return ans;
        }


        public List<PTWQuestionTypeAns> GetAllTypes()
        {
            return _context.PTWQuestionTypeAns.ToList();
        }
    }
}
