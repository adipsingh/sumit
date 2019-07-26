using System;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODS.PTW.Dtos;
using MODS.PTW.Helpers;
using MODS.PTW.Models;
using MODS.PTW.Services;

namespace MODS.PTW.Controllers
{
    [Route("api/media")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private IMediaService _mediaService;
        private IMapper _mapper;
         private IHostingEnvironment _hostingEnvironment;

        public MediaController(IMediaService mediaService, IMapper mapper, IHostingEnvironment environment)
        {
            _mediaService = mediaService;
            _mapper = mapper;
             _hostingEnvironment = environment;

        }
        //[AllowAnonymous]
        //[HttpPost("upload")]
        //public async Task<IActionResult> UploadMedia(IFormFile formFile)
        //{
        //    if (formFile != null || formFile.Length > 0)
        //    {
        //        return await _mediaService.Upload(formFile);
        //    }

        //        return NotFound();


        [HttpPost("Add")]
        public async Task<IActionResult> Addfile([FromBody]MediaDto file)
        {
            var media = _mapper.Map<Media>(file);
            
            try
            {
                string localPath="~/Media/";

                switch (file.FileType)
                {
                    case "Image":
                    localPath="~/Media/Image/";
                    break;
                    
                    default:
                    break;
                }
                
                string path = _hostingEnvironment.ContentRootPath+localPath;

                if (!Directory.Exists(path))
                {
                        Directory.CreateDirectory(path);
                }

                 //this image is a single pixel (black)
                byte[] bytes = Convert.FromBase64String(file.Body);

              
                using (MemoryStream ms = new MemoryStream(bytes))
                {  

                        var fileName = file.Name;

                        using (var fileStream = new FileStream(path + fileName, FileMode.CreateNew, FileAccess.ReadWrite))
                        {
                            ms.Position = 0;
                            ms.CopyTo(fileStream); // fileStream is not populated
                        }
                   
                }
                
                media.Path=path;


                var data = _mediaService.create(media);
                return Ok(data);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }




















        //try
        //{
        //    string PhotoPath = Convert.ToString(ConfigurationManager.AppSettings["ImagePath"]);

        //    Media newObj = new Media();

        //    newObj.Name = media.Name;
        //    newObj.Path = media.Path;
        //    newObj.Extention = media.Extention;
        //    newObj.UploadedOn = DateTime.Now;


        //    if (String.IsNullOrEmpty(newObj.Path))
        //    {

        //    }
        //    else
        //    {
        //        string startingFilePath = PhotoPath;

        //        string FilePath = _mediaService.Upload(newObj.Type, startingFilePath, newObj.Name);

        //        FileInfo fInfo = new FileInfo(FilePath);

        //        newObj.Type = fInfo.Name;
        //    }


        //    var newArticle = _context.(newObj.Title, newObj.Content,
        //    newObj.FileName, newObj.FilePath, newObj.FileLength);

        //    return Request.CreateResponse(HttpStatusCode.Created, newArticle);


        //}
        //catch(Exception)
        //{

        //}

    }
    
}