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
    [Route("api/WorkPermitStatus")]
    [ApiController]
    public class WorkPermitStatusController : ControllerBase
    {
        IWorkPermitStatusService _service;
        IMapper _mapper;

        public WorkPermitStatusController(IWorkPermitStatusService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

        }

        [HttpPost("add")]
        public IActionResult AddWorkPermitStatus([FromBody]WorkPermitStatusDto  workPermit)
        {
            var workPermitStatus = _mapper.Map<WorkPermitStatus>(workPermit);
            try
            {
                _service.Create(workPermitStatus);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("update/{id}")]
        public IActionResult UpdateWorkPermitStatus(int id, [FromBody] WorkPermitStatusDto workPermit)
        {
            var workPermitStatus = _mapper.Map<WorkPermitStatus>(workPermit);
            try
            {
                _service.Update(workPermitStatus, id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("All")]
        public IActionResult GetAllWorkPermitStatus()
        {
            var workPermitStatus = _service.GetAllWorkPermitStatus();
            List<WorkPermitStatusDto> Dto = new List<WorkPermitStatusDto>();
            foreach (var item in workPermitStatus)
            {
                WorkPermitStatusDto Dtos = _mapper.Map<WorkPermitStatusDto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
        }


        [HttpGet("getworkpermitstatus/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetStatusByID(int id)
        {
            var workPermitStatus = _service.GetById(id);
            if (workPermitStatus == null)
                return new UnauthorizedResult();
            WorkPermitStatusDto StatusDto = new WorkPermitStatusDto() { ID = workPermitStatus.ID, Status = workPermitStatus.Status };
            return Ok(StatusDto);
        }


        [HttpPost("delete/{id}")]
        public IActionResult DeleteWorkPermitStatus(int id, [FromBody] WorkPermitStatusDto workPermitDto)
        {
            var workPermitStatus = _mapper.Map<WorkPermitStatus>(workPermitDto);
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}