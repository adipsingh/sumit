using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface ICompanyCatagoryService
    {
        CompanyCatagory Create(CompanyCatagory companyCatagory);
        List<CompanyCatagory> GetAllCompanyCatagory();
        CompanyCatagory GetById(int id);
        CompanyCatagory Update(CompanyCatagory  companyCatagory, int id);
        CompanyCatagory Delete(int id);
    }
}
