using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Models
{
    public class PTWQuestion
    {
        public int ID { get; set; }
        public string Questions { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public ICollection<PTWQuestionTypeAns> Types {get;set;}
    }
}
