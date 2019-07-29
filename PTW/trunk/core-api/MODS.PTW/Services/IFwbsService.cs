using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface IFwbsService
    {
        Fwbs Create(Fwbs fwbs );
        List<Fwbs> GetAllFwbs();
        Fwbs GetById(int id);
        // Fwbs Update(int id, Fwbs question);
        // Fwbs Delete(int id);
        Fwbs Save(Fwbs fwbs);

        Fwbs GetByName(string name);

    }
}
