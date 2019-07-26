using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class CompanyCatagoryService : ICompanyCatagoryService
    {


        private DataContext _context;

        public CompanyCatagoryService(DataContext context)
        {
            _context = context;
        }
        public CompanyCatagory Create(CompanyCatagory companyCatagory)
        {
            if (string.IsNullOrEmpty(companyCatagory.Catagory))
            {
                throw new ArgumentException("company catagory should not be empty ");
            }
            if (_context.CompanyCategories.Any(x => x.Catagory == companyCatagory.Catagory))
            {
                throw new AppException("company catagory is already Exists");
            }
            _context.CompanyCategories.Add(companyCatagory);
            _context.SaveChanges();

            return companyCatagory;
        }



        public CompanyCatagory Delete(int id)
        {
            var data = _context.CompanyCategories.Find(id);
            _context.CompanyCategories.Remove(data);
            _context.SaveChanges();
            return data;
        }



        public List<CompanyCatagory> GetAllCompanyCatagory()
        {
            return _context.CompanyCategories.ToList();
        }




        public CompanyCatagory GetById(int id)
        {
            return _context.CompanyCategories.Find(id);
        }




        public CompanyCatagory Update(CompanyCatagory companyCatagory, int id)
        {
            var company = (from x in _context.CompanyCategories
                             where x.ID == companyCatagory.ID
                             select x).FirstOrDefault();

            if (_context.CompanyCategories.Any(x => x.Catagory == companyCatagory.Catagory))
            {
                throw new AppException("catagory is already Exists");
            }

            if (company.ID == id)
            {
                company.Catagory = companyCatagory.Catagory;
            }

           
            _context.CompanyCategories.Update(company);
            _context.SaveChanges();

            return company;
        }
    }
}
