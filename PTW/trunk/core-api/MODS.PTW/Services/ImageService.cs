using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Helpers;
using MODS.PTW.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace MODS.PTW.Services
{
    public class ImageService : IImageService
    {
        private DataContext _context;

        public ImageService(DataContext context)
        {
            _context = context;
        }
        public Image GetImage(int id)
        {
            throw new NotImplementedException();
        }

       

        public string Upload(string base64, string FilePath, string ImageName,DateTime uploadTime)
        {
            //Get the file type to save in
            var FilePathWithExtension = "";
            string localBase64 = "";

            if (base64.Contains("data:image/jpeg;base64,"))
            {
                FilePathWithExtension = FilePath + ImageName + ".jpg";
                localBase64 = base64.Replace("data:image/jpeg;base64,", "");
            }
            else if (base64.Contains("data:image/png;base64,"))
            {
                FilePathWithExtension = FilePath + ImageName + ".png";
                localBase64 = base64.Replace("data:image/png;base64,", "");
            }
            else if (base64.Contains("data:image/bmp;base64"))
            {
                FilePathWithExtension = FilePath + ImageName + ".bmp";
                localBase64 = base64.Replace("data:image/bmp;base64", "");
            }
            else if (base64.Contains("data:application/msword;base64,"))
            {
                FilePathWithExtension = FilePath + ImageName + ".doc";
                localBase64 = base64.Replace("data:application/msword;base64,", "");
            }
            else if (base64.Contains("data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,"))
            {
                FilePathWithExtension = FilePath + ImageName + ".docx";
                localBase64 = base64.Replace("data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,", "");
            }
            else if (base64.Contains("data:application/pdf;base64,"))
            {
                FilePathWithExtension = FilePath + ImageName + ".pdf";
                localBase64 = base64.Replace("data:application/pdf;base64,", "");
            }

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(localBase64)))
            {
                using (FileStream fs = new FileStream(FilePathWithExtension, FileMode.Create, FileAccess.Write))
                {
                    //Create the specified directory if it does not exist
                    var Images = Path.GetDirectoryName(FilePathWithExtension);
                    if (!Directory.Exists(Images))
                    {
                        Directory.CreateDirectory(Images);
                    }

                    ms.WriteTo(fs);
                    fs.Close();
                    ms.Close();
                }
            }

            return FilePathWithExtension;
        }
























        //Image imagefile = new Image();
           
        //   if(image.Length>0)
        //    {
        //        var imageName = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');

        //        byte[] fileBytes = null;
        //        using (var fileStream = image.OpenReadStream())
        //        using (var ms = new MemoryStream())
        //        {
        //            fileStream.CopyTo(ms);
        //            fileBytes = ms.ToArray();
                    
        //        }
        //        imagefile.Name = imageName;
        //        imagefile.Path = Convert.ToBase64String(fileBytes);
        //        imagefile.Type = image.ContentType;
        //        imagefile.UploadDate = DateTime.Now;
        //    }
        //    _context.images.Add(imagefile);
        //    _context.SaveChangesAsync();

        //    return imagefile;            
        //}
    }
}
