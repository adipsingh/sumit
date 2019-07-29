using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MODS.PTW.Models;
using MODS.PTW.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Helpers
{
    public class MediaHelperClass
    {
        

        public static Media CreateMedia(Media media,string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String(media.Body);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                var fileName = media.Name;
                using (var fileStream = new FileStream(path + fileName, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    ms.Position = 0;
                    ms.CopyTo(fileStream); // fileStream is not populated
                }
            }
            media.Path = path;
            
            return media;
        }
    }
}
