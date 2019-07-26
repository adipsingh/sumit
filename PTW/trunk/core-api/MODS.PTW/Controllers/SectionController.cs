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
    [Route("api/section")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private ISectionService _service;
        private IMapper _mapper;

        public SectionController(ISectionService service , IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpPost("Add")]
        public IActionResult AddSection([FromBody]SectionDto sectionDto)
        {
            var section = _mapper.Map<Section>(sectionDto);
            //Company company = new Company()
            //{
            //    Name = companyDto.Name,
            //    CategoryId = companyDto.CategoryId,
            //    Section = companyDto.Section,
            //    FunctionCode = companyDto.FunctionCode
            //};

            try
            {
                _service.Create(section);
                return Ok("Records Added Successfully.. ");
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("update/{id}")]
        public IActionResult UpdateSection(int id, [FromBody]SectionDto SectionDto)
        {
            var Section = _mapper.Map<Section>(SectionDto);
            try
            {
                _service.Update(Section, id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("All")]
        public IActionResult GetAllSection()
        {
            var section = _service.GetAllSection();
            List<SectionDto> Dto = new List<SectionDto>();
            foreach (var item in section)
            {
                SectionDto Dtos = _mapper.Map<SectionDto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
        }


        [HttpGet("getsection/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetSectionByID(int id)
        {
            var section = _service.GetById(id);
            if (section == null)
                return new UnauthorizedResult();

            SectionDto sectionDto = new SectionDto()
            {
                ID = section.ID,
                Name = section.Name
            };
            return Ok(sectionDto);
        }



        [HttpPost("delete/{id}")]
        public IActionResult DeleteSection(int id)
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
    }
}