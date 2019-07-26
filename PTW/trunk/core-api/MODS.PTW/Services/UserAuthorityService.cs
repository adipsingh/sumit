using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;

namespace MODS.PTW.Services
{
    public class UserAuthorityService : IUserAuthorityService
    {

        private DataContext _context;
        public UserAuthorityService(DataContext context)
        {
            _context = context;
            
        }


        public UserAuthority Create(UserAuthority userAuthority)
        {
            if (string.IsNullOrEmpty(userAuthority.Authority ))
            {
                throw new ArgumentException("user Authority should not be empty ");
            }
            if(string.IsNullOrEmpty(userAuthority.JGCITCode))
            {
                throw new ArgumentException("user Authority JGCITCode should not be empty ");
            }
            if (_context.UserAuthorities.Any(x => x.Authority == userAuthority.Authority))
            {
                throw new AppException("user Authority is already Exists");
            }
            _context.UserAuthorities.Add(userAuthority);
            _context.SaveChanges();

            return userAuthority;
        }

        public UserAuthority Delete(int id)
        {
            var data = _context.UserAuthorities.Find(id);
            _context.UserAuthorities.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public List<UserAuthority> GetAllUserAuthority()
        {
            return _context.UserAuthorities.ToList();
        }

        public UserAuthority GetById(int id)
        {
            return _context.UserAuthorities.Find(id);
        }

        public UserAuthority Update(UserAuthority userAuthority, int id)
        {
            var authority = (from x in _context.UserAuthorities
                              where x.ID == userAuthority.ID
                              select x).FirstOrDefault();

            if (authority.ID == id)
            {
                authority.Authority = userAuthority.Authority;
                authority.JGCITCode = userAuthority.JGCITCode;

            }

            if (_context.UserAuthorities.Any(x => x.Authority== userAuthority.Authority))
            {
                throw new AppException("User Authority is already Exists");
            }
            _context.UserAuthorities.Update(authority);
            _context.SaveChanges();

            return authority;
        }
    }
}
