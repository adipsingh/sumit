﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;
using Newtonsoft.Json;

namespace MODS.PTW.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;
        
        public UserService(DataContext context)
        {
            _context = context;
        }


       

        /* public User Authenticate(string email, string password)
         {
             if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                 return null;

             var user = _context.Users.SingleOrDefault(x => x.Email == email);

             if (user == null)
                 return null;

             if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                 return null;

             return user;
         }*/

         //public User GetById(int id)
         //{
         //    return _context.Users.Find(id);
         //}


        /* public List<User> GetAllUser()
         {
             return _context.Users.ToList();
         }*/

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

          //  if (_context.Users.Any(x => x.Email == user.Email))
              //  throw new AppException("Email \"" + user.Email + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Username = user.Email;
           // _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }


       /* public bool AddUserToRoles(User user, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                Role role = _context.Roles.FirstOrDefault(p => p.Name == roleName);
                if (role != null && _context.UserRoles.Select(p => p.UserId == user.Id && p.RoleId == role.Id).Count() == 0)
                {
                    _context.UserRoles.Add(new UserRole() { RoleId = role.Id, UserId = user.Id });
                }

            }
            _context.SaveChanges();
            return true;
        }*/
        


        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        
        //public static string GetUser(int id)
        //{
        //    var Name = (from x in _context.Users
        //               where x.Id ==id
        //               select x.FirstName).FirstOrDefault();
        //    return Name;
        //}

        /* public User Delete(int id)
         {
             var data = _context.Users.Find(id);
             _context.Users.Remove(data);
             _context.SaveChanges();
             return data;
         }*/


        /*  public User Update(User user, int id)
          {
              var userUpdate = (from x in _context.Users
                                where x.Id == user.Id
                                select x).FirstOrDefault();

              if (userUpdate.Id == id)
              {
                  userUpdate.FirstName = user.FirstName;
                  userUpdate.LastName = user.LastName;
                  userUpdate.Email = user.Email;
                  userUpdate.PhoneNumber = user.PhoneNumber;
                  userUpdate.Username = user.Username;

              }

             // if (_context.Users.Any(x => x.Email == user.Email))
             // {
              //    throw new AppException(" Email  " + user.Email + "  is already Exists");
            //  }
              _context.Users.Update(userUpdate);
              _context.SaveChanges();

              return userUpdate;
          }*/
    }
}
