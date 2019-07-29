using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Models
{
    public class Certificate
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> IsCertificateQA { get; set; }
        public ICollection<CertificateQuestion> CertificateQuestions { get; set; }
    }
}
