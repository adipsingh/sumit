using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Dtos
{
    public class FwbsUploadImageDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string  ImageName { get; set; }
        public string Body { get; set; }
        public string Extension { get; set; }
        public string FileType { get; set; }
    }
}
