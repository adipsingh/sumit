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
    [Route("api/certificate")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private ICertificateService _service;
        private IMapper _mapper;
        public CertificateController(ICertificateService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public IActionResult AddCertificate([FromBody]CertificateDto certificateDto)
        {
            var certificate = _mapper.Map<Certificate>(certificateDto);
            try
            {
                var data=_service.Create(certificate);
                return Ok(data);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("update/{id}")]
        public IActionResult UpdateCertificate(int id, [FromBody] CertificateDto certificateDto)
        {
            var certificate = _mapper.Map<Certificate>(certificateDto);
            try
            {
                var data = _service.Update(certificate, id);
                return Ok(data);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("all")]
        public IActionResult GetAllCertificates()
        {
            var certificate = _service.GetAllCertificate();
            List<CertificateDto> Dto = new List<CertificateDto>();
            foreach (var item in certificate)
            {
                CertificateDto Dtos = _mapper.Map<CertificateDto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
        }


        [HttpGet("get/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetCertificateByID(int id)
        {
            var certificate = _service.GetById(id);
            if (certificate == null)
                return new UnauthorizedResult();
            CertificateDto certificateDto = new CertificateDto()
            {
                ID = certificate.ID,
                Name = certificate.Name,
                ParentID = certificate.ParentID,
                CreatedBy = certificate.CreatedBy,
                CreatedOn=certificate.CreatedOn,
                ModifiedBy=certificate.ModifiedBy,
                ModifiedOn=certificate.ModifiedOn,
                Status=certificate.Status,
                IsCertificateQA = certificate.IsCertificateQA

            };
            return Ok(certificateDto);
        }

        [HttpPost("delete/{id}")]
        public IActionResult DeleteCertificate(int id)
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


        [HttpGet("allQACertificates")]
        public IActionResult GetAllQACertificates()
        {
            var certificate = _service.GetAllCertificate();
            List<CertificateGetAllDto> Dto = new List<CertificateGetAllDto>();
            foreach (var item in certificate)
            {
                if (item.IsCertificateQA == true)
                {
                    CertificateGetAllDto Dtos = new CertificateGetAllDto()
                    {
                        ID = item.ID,
                        Name = item.Name
                    };
                    Dto.Add(Dtos);
                }
            }
            return Ok(Dto);
        }
    }
}