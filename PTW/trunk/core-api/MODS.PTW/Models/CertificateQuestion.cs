using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Models
{
    public class CertificateQuestion
    {
        public int CertificateID { get; set; }
        public int QuestionID { get; set; }       
        public virtual Certificate Certificate { get; set; }
        public virtual Question Question { get; set; }
    }
}
