﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{

    
    public class CompanyService : ICompanyService
    {


        private DataContext _context;
        public CompanyService(DataContext context)
        {
            _context = context;
        }

         public Company Create(Company company)
        {
            if (string.IsNullOrEmpty(company.Name))
            {
                throw new ArgumentException("company Name should not be empty ");
            }
            
            if(string.IsNullOrEmpty(company.Code))
            {
                throw new ArgumentException("Company Code should not be empty");
            }
            if (_context.Companies.Any(x => x.Name == company.Name))
            {
                throw new AppException("company name is already Exists");
            }
            _context.Companies.Add(company);
            _context.SaveChanges();

            return company;
        }

        public Company Delete(int id)
        {
            var data = _context.Companies.Find(id);
            _context.Companies.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public List<Company> GetAllCompany()
        {
            return _context.Companies.ToList();
        }

        public Company GetById(int id)
        {
            return _context.Companies.Find(id);
        }

        public Company Update(Company company, int id)
        {
            var comp = (from x in _context.Companies
                           where x.ID == id
                           select x).FirstOrDefault();
            if (_context.Companies.Any(x => x.Name == company.Name))
            {
                throw new AppException("company name is already Exists");
            }
            if(string.IsNullOrEmpty(company.Name) || string.IsNullOrEmpty(company.Code))
            {
                throw new ArgumentException("Company Detail should not be Empty");
            }
            if (comp.ID == id)
            {
                comp.Name= company.Name;                
                comp.Code = company.Code;                
            }            
            _context.Companies.Update(comp);
            _context.SaveChanges();

            return comp;
        }
    }
}
