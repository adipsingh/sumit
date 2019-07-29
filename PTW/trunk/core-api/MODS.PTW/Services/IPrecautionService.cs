using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface IPrecautionService
    {
        PrecautionQA Create(PrecautionQA question);
        List<PrecautionQA> GetAllQuestions();
        PrecautionQA GetById(int id);
        PrecautionQA GetByTypeId(int id);
        PrecautionQA Update(int id, PrecautionQA question);
        PrecautionQA Delete(int id);
    }
}
