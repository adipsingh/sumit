using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Dtos
{
    public class ImageDto
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
