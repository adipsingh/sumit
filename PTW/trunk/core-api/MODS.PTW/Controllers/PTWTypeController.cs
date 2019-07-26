using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODS.PTW.Dtos;
using MODS.PTW.Helpers;
using MODS.PTW.Models;
using MODS.PTW.Services;

namespace MODS.PTW.Controllers
{
    [Route("api/ptwtype")]
    [ApiController]
    public class PTWTypeController : ControllerBase
    {
        private IPTWTypeService _service;
        private IMapper _mapper;

        public PTWTypeController(IPTWTypeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public IActionResult AddType([FromBody]PTWTypeDto typeDto)
        {
            var type = _mapper.Map<PTWType>(typeDto);
            try
            {
                _service.Create(type);
                return Ok("Records Added Successfully.. ");
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpPost("update/{id}")]
        public IActionResult UpdateType(int id, [FromBody]PTWTypeDto typeDto)
        {
            var type = _mapper.Map<PTWType>(typeDto);
            try
            {
                _service.Update(id, type);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("delete/{id}")]
        public IActionResult DeleteType(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("Record Deleted Successfully..");
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("All")]
        public IActionResult GetAllType()
        {
            var type = _service.GetAllTypes();
            List<PTWTypeDto> Dto = new List<PTWTypeDto>();
            foreach (var item in type)
            {
                PTWTypeDto Dtos = _mapper.Map<PTWTypeDto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
        }


        [HttpGet("gettype/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetTypeByID(int id)
        {
            var type = _service.GetById(id);
            if (type == null)
                return new UnauthorizedResult();

            PTWTypeDto typeDto = new PTWTypeDto()
            {
                ID = type.ID,
                Type = type.Type
            };
            return Ok(typeDto);
        }
    }
}