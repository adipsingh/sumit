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
    [Route("api/function")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        private IFunctionService _service;
        private IMapper _mapper;

        public FunctionController(IFunctionService service , IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpPost("Add")]
        public IActionResult AddFunction([FromBody]FunctionDto functionDto)
        {
            var function = _mapper.Map<Function>(functionDto);
            //Company company = new Company()
            //{
            //    Name = companyDto.Name,
            //    CategoryId = companyDto.CategoryId,
            //    Section = companyDto.Section,
            //    FunctionCode = companyDto.FunctionCode
            //};

            try
            {
                _service.Create(function);
                return Ok("Records Added Successfully.. ");
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpPost("update/{id}")]
        public IActionResult UpdateFunction(int id, [FromBody]FunctionDto functionDto)
        {
            var function = _mapper.Map<Function>(functionDto);
            
            try
            {
                _service.Update(function, id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("All")]
        public IActionResult GetAllFunction()
        {
            var function = _service.GetAllFunction();
            List<FunctionDto> Dto = new List<FunctionDto>();
            foreach (var item in function)
            {
                FunctionDto Dtos = _mapper.Map<FunctionDto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
        }


        [HttpGet("getfunction/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetFunctionByID(int id)
        {
            var function = _service.GetById(id);
            if (function == null)
                return new UnauthorizedResult();

            FunctionDto functionDto = new FunctionDto()
            {
                ID = function.ID,
                Name = function.Name
            };
            return Ok(functionDto);
        }



        [HttpPost("delete/{id}")]
        public IActionResult DeleteFunction(int id, [FromBody]FunctionDto functionDto)
        {
            var function = _mapper.Map<Function>(functionDto);
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