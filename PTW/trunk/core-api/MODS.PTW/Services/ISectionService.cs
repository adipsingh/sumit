using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface ISectionService
    {
        Section Create(Section section);
        List<Section> GetAllSection();
        Section GetById(int id);
        Section Update(Section section, int id);
        Section Delete(int id);
    }
}
