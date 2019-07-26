using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface IPTWQuestiontypeAnsService
    {
        PTWQuestionTypeAns create(PTWQuestionTypeAns pTWQuestionAns);
        PTWQuestionTypeAns update(int id, PTWQuestionTypeAns pTWQuestionAns);
        List<PTWQuestionTypeAns> GetAllTypes();
    }
}
