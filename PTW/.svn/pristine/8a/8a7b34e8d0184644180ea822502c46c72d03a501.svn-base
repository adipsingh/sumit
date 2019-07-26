using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODS.PTW.Dtos;
using MODS.PTW.Helpers;
using MODS.PTW.Models;
using MODS.PTW.Services;

namespace MODS.PTW.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IImageService _imageService;
        private IMapper _mapper;

        public ImageController(IImageService imageService, IMapper mapper)
        {
            _imageService = imageService;
            _mapper = mapper;

        }
       /* [AllowAnonymous]
        [HttpPost("upload")]
        public HttpResponseMessage PostImage([FromBody]Image image)
        {
            try
            {
                string PhotoPath = Convert.ToString(ConfigurationManager.AppSettings["ImagePath"]);

                Image newObj = new Image();

                newObj.Name = image.Name;
                newObj.Path = image.Path;                               
                newObj.Type = image.Type;
                newObj.UploadDate = image.UploadDate;

                if (String.IsNullOrEmpty(newObj.Path))
                {

                }
                else
                {
                    string startingFilePath = PhotoPath;

                    string FilePath = _imageService.Upload(newObj.Type, startingFilePath, newObj.Name, newObj.UploadDate);

                    FileInfo fInfo = new FileInfo(FilePath);

                    newObj.Type = fInfo.Name;
                }


                var newArticle = _context.(newObj.Title, newObj.Content,
                newObj.FileName, newObj.FilePath, newObj.FileLength);

                return Request.CreateResponse(HttpStatusCode.Created, newArticle);
               

            }
            catch(Exception)
            {

            }
           
        }*/
    }
}