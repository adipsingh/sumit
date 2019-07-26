using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MODS.PTW.Models
{
    public class User
    {
        public User()
        {
            this.Roles = new HashSet<UserRole>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] EmailConfirmed { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PhoneNumberConfirmed { get; set; }
        public byte[] TwoFactorEnabled { get; set; }
        public DateTime LockoutEndDateUtc { get; set; }
        public byte[] LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; }
    }
}
