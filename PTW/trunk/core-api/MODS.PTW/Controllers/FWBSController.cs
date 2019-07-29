using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODS.PTW.Dtos;
using MODS.PTW.Helpers;
using MODS.PTW.Models;
using MODS.PTW.Services;
using Newtonsoft.Json.Linq;

namespace MODS.PTW.Controllers
{
    [Route("api/fwbs")]
    [ApiController]
    public class FWBSController : ControllerBase
    {
        private IFwbsService _service;
        private IMediaService _mediaService;
        private IMapper _mapper;
        private IHostingEnvironment _hostingEnvironment;
        public FWBSController(IFwbsService service, IMapper mapper, IMediaService mediaService, IHostingEnvironment hostingEnvironment )
        {
            _service = service;
            _mapper = mapper;
            _mediaService = mediaService;
            _hostingEnvironment = hostingEnvironment;

        }


        [HttpPost("Add")]
        public IActionResult AddQuestion([FromBody]FwbsDto questionDto)
        {
            var question = _mapper.Map<Fwbs>(questionDto);
            try
            {
                _service.Create(question);
                return Ok("Records Added Successfully.. ");
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpGet("All")]
        public IActionResult GetAllFwbs()//[FromBody]JObject jobject
        {
            var fwbs = _service.GetAllFwbs();
            List<FwbsDto> Dto = new List<FwbsDto>();
            foreach (var item in fwbs)
            {
                FwbsDto Dtos = _mapper.Map<FwbsDto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
            //return Ok(new
            //{
            //    items = Dto,
            //    totalCount = Dto.Count(),
            //    errorMessage = string.Empty
            //});
        }

        [HttpGet("Allfwbsname")]
        public IActionResult GetAllFwbsName()//[FromBody]JObject jobject
        {
            var fwbs = _service.GetAllFwbs();
            List<string> Dto = new List<string>();
            foreach (var item in fwbs)
            {
                FwbsDto Dtos = _mapper.Map<FwbsDto>(item);
                Dto.Add(Dtos.Name);
            }
            return Ok(Dto);
            //return Ok(new
            //{
            //    items = Dto,
            //    totalCount = Dto.Count(),
            //    errorMessage = string.Empty
            //});
        }


        [HttpGet("getfwbs/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetFwbsByID(int id)
        {
            var fwbs = _service.GetById(id);
            if (fwbs == null)
                return new UnauthorizedResult();

            FwbsDto fwbdDto = new FwbsDto()
            {
                ID = fwbs.ID,
                MediaID = fwbs.MediaID,
                Name = fwbs.Name,
                CreatedBy = fwbs.CreatedBy,
                CreatedOn = fwbs.CreatedOn,
                ModifiedBy = fwbs.ModifiedBy,
                ModifiedOn = fwbs.ModifiedOn,

            };
            return Ok(fwbdDto);
        }


        [HttpPost("AddMedia")]
        public IActionResult Addfile([FromBody]FwbsUploadImageDto file)
        {
           // var media = _mapper.Map<Media>(file);
            string localPath = "\\Media\\";
            switch (file.FileType)
            {
                case "Image":
                    localPath = "\\Media\\Image\\";
                    break;
                default: break;
            }
            Media media = new Media()
            {
                Name = file.ImageName,
                Extention = file.Extension,
                FileType = file.FileType,
                Body = file.Body
            };

            string path = _hostingEnvironment.ContentRootPath + localPath;
            var dataFromHelper = MediaHelperClass.CreateMedia(media, path);
            var data = _mediaService.create(dataFromHelper);

            Fwbs fwbsInfo = new Fwbs()
            {
                Name = file.Name,
                MediaID = data.ID
            };
            var createData = _service.Save(fwbsInfo);            
            return Ok(createData);
        }



        [HttpGet("getfwbsmedia/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetFwbsMediaByName(string name)
        {
            string url= null;
            var fwbs = _service.GetByName(name);
            var media = _mediaService.Download(fwbs.MediaID);
            if (fwbs == null )
                return new UnauthorizedResult();
            if (fwbs.Name == name)
            {
                url = media.Path + media.Name;
            }
            
            return Ok(url);
        }
    }
}