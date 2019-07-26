using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface IWorkPermitTypesService
    {
        WorkPermitTypes Create(WorkPermitTypes workPermitTypes);
        List<WorkPermitTypes> GetAllWorkPermitTypes();
        WorkPermitTypes GetById(int id);
        WorkPermitTypes Update(WorkPermitTypes workPermitTypes, int id);
        WorkPermitTypes Delete(int id);
    }
}
