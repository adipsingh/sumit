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
    [Route("api/ptwanswers")]
    [ApiController]
    public class PTWQuestionTypeAnsController : ControllerBase
    {
        private IPTWQuestiontypeAnsService _service;
        private IMapper _mapper;

        public PTWQuestionTypeAnsController(IPTWQuestiontypeAnsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public IActionResult AddAnswers([FromBody]PTWQuestionTypeAnsDto ansDto)
        {
            var answers = _mapper.Map<PTWQuestionTypeAns>(ansDto);
            try
            {
                var ans=_service.create(answers);
                return Ok(ans);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("update/{id}")]
        public IActionResult UpdateAnswers(int id, [FromBody]PTWQuestionTypeAnsDto ansDto)
        {
            var answers = _mapper.Map<PTWQuestionTypeAns>(ansDto);
            try
            {
                var ans=_service.update(id, answers);
                return Ok(ans);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("All")]
        public IActionResult GetAnswers()
        {
            var qa = _service.GetAllTypes();
            List<PTWQuestionTypeAnsDto> Dto = new List<PTWQuestionTypeAnsDto>();
            foreach (var item in qa)
            {
                PTWQuestionTypeAnsDto Dtos = _mapper.Map<PTWQuestionTypeAnsDto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
        }


    }
}