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
using Microsoft.AspNetCore.Mvc;

namespace MODS.PTW.Services
{
    public class MediaService : IMediaService
    {
        private DataContext _context;

        public MediaService(DataContext context)
        {
            _context = context;
        }

        public async Task<string> create(Media media)
        {
            // string filename;
            // var Ext = "." + media.Name.Split('.')[media.Name.Split('.').Length - 1];
            // filename = media.Name;
            // var path = Path.Combine(Directory.GetCurrentDirectory(), "Media", filename);
            // using (var bit =new FileStream(path,FileMode.Create))
            // {
            //     await media.CopyToAsync(bit);
            // }

            Media imagefile = media;
            //imagefile.Name = filename;
           // imagefile.Path = path;
           // imagefile.Extention = "." + media.FileName.Split('.')[media.FileName.Split('.').Length - 1];
            imagefile.UploadedOn = DateTime.Now;
            //if (media.Length > 0)
            //{
            //    var imageName = ContentDispositionHeaderValue.Parse(media.ContentDisposition).FileName.Trim('"');

            //    byte[] fileBytes = null;
            //    using (var fileStream = media.OpenReadStream())
            //    using (var ms = new MemoryStream())
            //    {
            //        fileStream.CopyTo(ms);
            //        fileBytes = ms.ToArray();

            //    }
            //    imagefile.Name = imageName;
            //    imagefile.Path = Convert.ToBase64String(fileBytes);
            //    imagefile.Extention = "." + media.FileName.Split('.')[media.FileName.Split('.').Length - 1];
            //    imagefile.UploadedOn = DateTime.Now;

            //}
            _context.Media.Add(imagefile);
            _context.SaveChangesAsync();

            return media.Name;
            
        }

        public Media delete(int id)
        {
            var data = _context.Media.Find(id);
            _context.Media.Remove(data);
            _context.SaveChanges();
            return data;
        }

        public Media Download(int id)
        {
            throw new NotImplementedException();
        }

        public Media update(Media media, int id)
        {
            var file = (from x in _context.Media
                        where x.ID == id
                         select x).FirstOrDefault();
            if (_context.Media.Any(x => x.Name == media.Name))
            {
                throw new AppException("media is already Exists");
            }
            if (string.IsNullOrEmpty(media.Name))
            {
                throw new ArgumentException("media Name Should not be empty");
            }
            if (file.ID == id)
            {
                file.Name = media.Name;
            }
            _context.Media.Update(file);
            _context.SaveChanges();

            return file;
        }

        public Task<IActionResult> Upload(Media formFile)
        {
            //string filename;

            //    var Ext = "." + formFile.FileName.Split('.')[formFile.FileName.Split('.').Length - 1];
            //    filename = formFile.FileName;
            //    var path = Path.Combine(Directory.GetCurrentDirectory(), "Media",filename);
            throw new NotImplementedException();

        }




        //public string Upload(string base64, string FilePath, string ImageName,DateTime uploadTime)
        //{
        //    //Get the file type to save in
        //    var FilePathWithExtension = "";
        //    string localBase64 = "";

        //    if (base64.Contains("data:image/jpeg;base64,"))
        //    {
        //        FilePathWithExtension = FilePath + ImageName + ".jpg";
        //        localBase64 = base64.Replace("data:image/jpeg;base64,", "");
        //    }
        //    else if (base64.Contains("data:image/png;base64,"))
        //    {
        //        FilePathWithExtension = FilePath + ImageName + ".png";
        //        localBase64 = base64.Replace("data:image/png;base64,", "");
        //    }
        //    else if (base64.Contains("data:image/bmp;base64"))
        //    {
        //        FilePathWithExtension = FilePath + ImageName + ".bmp";
        //        localBase64 = base64.Replace("data:image/bmp;base64", "");
        //    }
        //    else if (base64.Contains("data:application/msword;base64,"))
        //    {
        //        FilePathWithExtension = FilePath + ImageName + ".doc";
        //        localBase64 = base64.Replace("data:application/msword;base64,", "");
        //    }
        //    else if (base64.Contains("data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,"))
        //    {
        //        FilePathWithExtension = FilePath + ImageName + ".docx";
        //        localBase64 = base64.Replace("data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,", "");
        //    }
        //    else if (base64.Contains("data:application/pdf;base64,"))
        //    {
        //        FilePathWithExtension = FilePath + ImageName + ".pdf";
        //        localBase64 = base64.Replace("data:application/pdf;base64,", "");
        //    }

        //    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(localBase64)))
        //    {
        //        using (FileStream fs = new FileStream(FilePathWithExtension, FileMode.Create, FileAccess.Write))
        //        {
        //            //Create the specified directory if it does not exist
        //            var Images = Path.GetDirectoryName(FilePathWithExtension);
        //            if (!Directory.Exists(Images))
        //            {
        //                Directory.CreateDirectory(Images);
        //            }

        //            ms.WriteTo(fs);
        //            fs.Close();
        //            ms.Close();
        //        }
        //    }

        //    return FilePathWithExtension;
        //}
























                
       
    }
}
