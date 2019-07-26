using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface IAreaService
    {
        Area Create(Area area);
        Area Update(Area area, int id);
        Area Delete(int id);
        List<Area> GetAllArea();
        Area GetById(int id);        
    }
}
