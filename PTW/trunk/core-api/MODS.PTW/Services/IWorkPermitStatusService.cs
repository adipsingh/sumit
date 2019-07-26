using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface IWorkPermitStatusService
    {
        WorkPermitStatus Create(WorkPermitStatus workPermitStatus);
        List<WorkPermitStatus> GetAllWorkPermitStatus();
        WorkPermitStatus GetById(int id);
        WorkPermitStatus Update(WorkPermitStatus workPermitStatus, int id);
        WorkPermitStatus Delete(int id);
    }
}
