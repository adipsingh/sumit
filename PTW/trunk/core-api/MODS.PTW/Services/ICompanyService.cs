﻿using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface ICompanyService
    {
        Company Create(Company company);
        List<Company> GetAllCompany();
        Company GetById(int id);
        Company Update(Company company, int id);
        Company Delete(int id);
    }
}
