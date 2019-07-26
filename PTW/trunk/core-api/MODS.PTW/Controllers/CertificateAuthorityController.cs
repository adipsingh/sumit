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
    [Route("api/certificateauthority")]
    [ApiController]
    public class CertificateAuthorityController : ControllerBase
    {
        private ICertificateAuthorityService _service;
        private IUserService _userService;
        private IMapper _mapper;
        public CertificateAuthorityController(ICertificateAuthorityService service,IMapper mapper, IUserService userService)
        {
            _service = service;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("Add")]
        public IActionResult AddCertificateAuthority([FromBody]CertificateAuthorityDto authorityDto)
        {
            var authority = _mapper.Map<CertificateAuthority>(authorityDto);
           // //var user = _userService.GetById(Convert.ToInt32(User.Identity.Name));
           //var  userName = HttpContext.User.Identity.Name;
            //authority.CreatedBy = userName;
            //authority.ModifiedBy = User.Identity.Name;
            try
            {
                var data = _service.create(authority);
                return Ok(data);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("update/{id}")]
        public IActionResult UpdateAuthority(int id, [FromBody] CertificateAuthorityDto authorityDto)
        {
            var authority = _mapper.Map<CertificateAuthority>(authorityDto);
            try
            {
                var data = _service.update(authority, id);
                return Ok(data);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("All")]
        public IActionResult GetAllCertificateAuthority()
        {
            var authority = _service.GetAllCertificateAuthority();
            List<CertificateAuthorityDto> Dto = new List<CertificateAuthorityDto>();
            foreach (var item in authority)
            {
                CertificateAuthorityDto Dtos = _mapper.Map<CertificateAuthorityDto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
        }


        [HttpGet("getcertificateauthority/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetCertificateAuthorityByID(int id)
        {
            var authority = _service.GetById(id);
            if (authority == null)
                return new UnauthorizedResult();
            CertificateAuthorityDto authorityDto = new CertificateAuthorityDto()
            {
                ID = authority.ID,
                CertificateID = authority.CertificateID,
                CompanyID = authority.CompanyID,
                SectionID=authority.SectionID,
                FunctionID=authority.FunctionID,
                EmployeeID=authority.EmployeeID,
                CreatedBy = authority.CreatedBy,
                CreatedOn = authority.CreatedOn,
                ModifiedBy = authority.ModifiedBy,
                ModifiedOn = authority.ModifiedOn,
                Status = authority.Status

            };
            return Ok(authorityDto);
        }

        [HttpPost("delete/{id}")]
        public IActionResult DeleteCertificateAuthority(int id)
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