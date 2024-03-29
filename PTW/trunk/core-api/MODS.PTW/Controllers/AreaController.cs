﻿using System;
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
    [Route("api/area")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private IAreaService _service;
        private IMapper _mapper;

        public AreaController(IAreaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public IActionResult AddArea([FromBody]AreaDto areaDto)
        {
            var area = _mapper.Map<Area>(areaDto);
            try
            {
                _service.Create(area);
                return Ok("Records Added Successfully.. ");
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("update/{id}")]
        public IActionResult UpdateArea(int id, [FromBody]AreaDto areaDto)
        {
            var area = _mapper.Map<Area>(areaDto);

            try
            {
                _service.Update(area, id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("All")]
        public IActionResult GetAllArea()
        {
            var area = _service.GetAllArea();
            List<AreaDto> Dto = new List<AreaDto>();
            foreach (var item in area)
            {
                AreaDto Dtos = _mapper.Map<AreaDto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
        }


        [HttpGet("getarea/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetAreaByID(int id)
        {
            var area = _service.GetById(id);
            if (area == null)
                return new UnauthorizedResult();

            AreaDto areaDto = new AreaDto()
            {
                ID = area.ID,
                Name = area.Name
            };
            return Ok(areaDto);
        }



        [HttpPost("delete/{id}")]
        public IActionResult DeleteArea(int id, [FromBody]AreaDto areaDto)
        {
            var area = _mapper.Map<Area>(areaDto);
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

    }
}