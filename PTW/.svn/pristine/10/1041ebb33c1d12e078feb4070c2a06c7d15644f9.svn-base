using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class FunctionService : IFunctionService
    {

        private DataContext _context;
        public FunctionService(DataContext context)
        {
            _context = context;
        }
        public Function Create(Function function)
        {
            if (string.IsNullOrEmpty(function.Name))
            {
                throw new ArgumentException("Function Name should not be empty ");
            }            
            if (_context.Functions.Any(x => x.Name == function.Name))
            {
                throw new AppException("Function name is already Exists");
            }
            _context.Functions.Add(function);
            _context.SaveChanges();

            return function;
        }

        public Function Delete(int id)
        {
            var data = _context.Functions.Find(id);
            _context.Functions.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public List<Function> GetAllFunction()
        {
            return _context.Functions.ToList();
        }

        public Function GetById(int id)
        {
            return _context.Functions.Find(id);
        }

        public Function Update(Function function, int id)
        {
            var fun = (from x in _context.Functions
                        where x.ID == function.ID
                        select x).FirstOrDefault();

            if (string.IsNullOrEmpty(function.Name))
            {
                throw new ArgumentException("Funtion name should not be Empty ");
            }
            if (fun.ID == id)
            {
                fun.Name = function.Name;  
            }

            if (_context.Functions.Any(x => x.Name == function.Name))
            {
                throw new AppException("Funtion name is already Exists");
            }
            _context.Functions.Update(fun);
            _context.SaveChanges();

            return fun;
        }
    }
}
