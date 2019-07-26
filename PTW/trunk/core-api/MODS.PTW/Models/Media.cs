using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Models
{
    public class Media
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Extention { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string UploadedBy { get; set; }
        public Nullable<DateTime> UploadedOn { get; set; }
        

    }
}
