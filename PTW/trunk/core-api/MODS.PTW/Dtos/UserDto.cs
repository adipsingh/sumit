using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }

    public class ShareUser
    {
        
        public string Email { get; set; }
      
    }

    public class ShareWith
    {
        public ShareUser User { get; set; }
    }


    public class Parent
    {

        public string Id { get; set; }

    }
    public class ShareObject
    {

        public string ShareType { get; set; }
        public string Title { get; set; }
        public List<ShareWith> Recipients { get; set; }
        public List<Parent> Items { get; set; }
       // public Parent Parent { get; set; }
        public string ExpirationDate { get; set; }
        public bool RequireLogin { get; set; }
        public bool RequireUserInfo { get; set; }
      //  public DateTime TrackUntilDate { get; set; }
        
    //    public int SendFrequency { get; set; }
      //  public int SendInterval { get; set; }
        public int MaxDownloads { get; set; }
        public bool UsesStreamIDs { get; set; }
    }
}