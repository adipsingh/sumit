using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Models
{
    public class PrecautionQA
    {
        public int ID { get; set; }
        public int PrecautionTypeID { get; set; }
        public string Question { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public virtual PrecautionType precautionType { get; set;}
    }
}
