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
    [Route("api/certificateqa")]
    [ApiController]
    public class CertificateQAController : ControllerBase
    {
        private ICertificateQAService _service;
        private IMapper _mapper;
        public CertificateQAController(ICertificateQAService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpPost("Add")]
        public IActionResult AddCertificateQA([FromBody]CertificateQADto  qADto)
        {
            var qa = _mapper.Map<CertificateQA>(qADto);
            try
            {
                var data = _service.create(qa);
                return Ok(data);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("update/{id}")]
        public IActionResult UpdateCertificateQA(int id, [FromBody] CertificateQADto qADto)
        {
            var qa = _mapper.Map<CertificateQA>(qADto);
            try
            {
                var data = _service.update(qa, id);
                return Ok(data);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("All")]
        public IActionResult GetAllCertificateQA()
        {
            var qa = _service.GetCertificateQA();
            List<CertificateQADto> Dto = new List<CertificateQADto>();
            foreach (var item in qa)
            {
                CertificateQADto Dtos = _mapper.Map<CertificateQADto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
        }


        [HttpGet("getcertificate/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetCertificateQAByID(int id)
        {
            var qa = _service.GetById(id);
            if (qa == null)
                return new UnauthorizedResult();
            CertificateQADto qADto = new CertificateQADto()
            {
                ID = qa.ID,
                Questions = qa.Questions,
                IsExcavation = qa.IsExcavation,
                IsConfinedSpace=qa.IsConfinedSpace,
                IsRadioGraphy=qa.IsRadioGraphy,
                IsEnergization=qa.IsEnergization,
                IsElectrical=qa.IsElectrical,
                IsCriticalLift=qa.IsCriticalLift,
                IsGratingRemoval=qa.IsGratingRemoval,
                createdBy = qa.createdBy,
                CreatedOn = qa.CreatedOn,
                ModifiedBy = qa.ModifiedBy,
                ModifiedOn = qa.ModifiedOn,
            };
            return Ok(qADto);
        }

        [HttpPost("delete/{id}")]
        public IActionResult DeleteCertificateQA(int id)
        {
            try
            {
                _service.delete(id);
                return Ok("Record Deleted Successfully..");
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}