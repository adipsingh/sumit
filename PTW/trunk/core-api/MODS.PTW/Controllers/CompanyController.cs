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
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ICompanyService _service;
        private IMapper _mapper;

        public CompanyController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }



        [HttpPost("Add")]
        public IActionResult AddCompany([FromBody]CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);
            //Company company = new Company()
            //{
            //    Name = companyDto.Name,
            //    CategoryID = companyDto.CategoryID,                
            //    Code = companyDto.Code
            //};

            try
            {
                _service.Create(company);
                return Ok("Records Added Successfully.. ");
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpPost("update/{id}")]
        public IActionResult UpdateCompany(int id, [FromBody] CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);
            try
            {
                _service.Update(company, id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpGet("All")]
        public IActionResult GetAllCompany()
        {
            var company = _service.GetAllCompany();
            List<CompanyDto> Dto = new List<CompanyDto>();
            foreach (var item in company)
            {
                CompanyDto Dtos = _mapper.Map<CompanyDto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
        }



        [HttpGet("getcompany/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetCompanyByID(int id)
        {
            var company = _service.GetById(id);
            if (company == null)
                return new UnauthorizedResult();
            CompanyDto companyDto = new CompanyDto()
            {
                ID = company.ID,
                Name = company.Name,
                CategoryID =company.CategoryID,               
                Code =company.Code
            };
            return Ok(companyDto);
        }




        [HttpPost("delete/{id}")]
        public IActionResult DeleteCompany(int id)
        {
            //var company = _mapper.Map<Company>(companyDto);
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