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
        

        [HttpPost("Add")]
        public  IActionResult Addfile([FromBody]MediaDto file)
        {
            var media = _mapper.Map<Media>(file);
            string localPath = "\\Media\\";
            switch (media.FileType)
            {
                case "Image":
                    localPath = "\\Media\\Image\\";
                    break;
                default: break;
            }

            string path = _hostingEnvironment.ContentRootPath + localPath;
            var dataFromHelper=MediaHelperClass.CreateMedia(media,path);
            var data = _mediaService.create(dataFromHelper);
            return Ok(data);                        
        }
       
    }
    
}