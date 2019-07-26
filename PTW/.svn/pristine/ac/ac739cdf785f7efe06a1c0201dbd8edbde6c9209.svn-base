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
    [Route("api/companycatagory")]
    [ApiController]
    public class CompanyCatagoryController : ControllerBase
    {
        private ICompanyCatagoryService  _service;
        private IMapper _mapper;

        public CompanyCatagoryController(ICompanyCatagoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public IActionResult AddCompanyCatagory ([FromBody]CompanyCatagoryDto companyDto)
        {
            var company = _mapper.Map<CompanyCatagory>(companyDto);
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
        public IActionResult UpdateCompanycatagory(int id, [FromBody] CompanyCatagoryDto companyDto)
        {
            var company = _mapper.Map<CompanyCatagory>(companyDto);
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
        public IActionResult GetAllCompanyCatagory()
        {
            var company = _service.GetAllCompanyCatagory();
            List<CompanyCatagoryDto> Dto = new List<CompanyCatagoryDto>();
            foreach (var item in company)
            {
                CompanyCatagoryDto Dtos = _mapper.Map<CompanyCatagoryDto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
        }



        [HttpGet("getcompanycatagory/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetCompanyCatagoryByID(int id)
        {
            var company = _service.GetById(id);
            if (company == null)
                return new UnauthorizedResult();
            CompanyCatagoryDto catagoryDto = new CompanyCatagoryDto() { ID = company.ID, Catagory = company.Catagory };
            return Ok(catagoryDto);
        }




        [HttpPost("delete/{id}")]
        public IActionResult DeleteCompanyCatagory(int id)
        {
            //var company = _mapper.Map<CompanyCatagory>(companyDto);
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