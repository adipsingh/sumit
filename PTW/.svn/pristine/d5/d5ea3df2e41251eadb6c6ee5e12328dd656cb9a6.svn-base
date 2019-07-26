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
    [Route("api/Workpermittypes")]
    [ApiController]
    public class WorkPermitTypesController : ControllerBase
    {
        IWorkPermitTypesService _service;
        IMapper _mapper;

        public WorkPermitTypesController(IWorkPermitTypesService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpPost("Add")]
        public IActionResult AddWorkPermitStatus([FromBody]WorkPermitTypesDto workPermit)
        {
            var workPermitType = _mapper.Map<WorkPermitTypes>(workPermit);
            try
            {
                _service.Create(workPermitType);
                return Ok("Records Added Successfully.. ");
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("update/{id}")]
        public IActionResult UpdateWorkPermitTypes(int id, [FromBody] WorkPermitTypesDto workPermit)
        {
            var workPermitType = _mapper.Map<WorkPermitTypes>(workPermit);
            try
            {
                _service.Update(workPermitType, id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("All")]
        public IActionResult GetAllWorkPermitTypes()
        {
            var workPermitType = _service.GetAllWorkPermitTypes();
            List<WorkPermitTypesDto> Dto = new List<WorkPermitTypesDto>();
            foreach (var item in workPermitType)
            {
                WorkPermitTypesDto Dtos = _mapper.Map<WorkPermitTypesDto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
        }


        [HttpGet("getworkpermitType/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetTypeByID(int id)
        {
            var workPermitType = _service.GetById(id);
            if (workPermitType == null)
                return new UnauthorizedResult();
            WorkPermitTypesDto TypesDto = new WorkPermitTypesDto() { ID = workPermitType.ID, PermitType = workPermitType.PermitType };
            return Ok(TypesDto);
        }


        [HttpPost("delete/{id}")]
        public IActionResult DeleteWorkPermitType(int id, [FromBody] WorkPermitTypesDto workPermitDto)
        {
            var workPermitType = _mapper.Map<WorkPermitTypes>(workPermitDto);
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