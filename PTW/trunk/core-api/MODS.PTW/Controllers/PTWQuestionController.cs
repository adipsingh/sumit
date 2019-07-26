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
using Newtonsoft.Json.Linq;

namespace MODS.PTW.Controllers
{
    [Route("api/ptwquestion")]
    [ApiController]
    public class PTWQuestionController : ControllerBase
    {
        private IPTWQuestionService _service;
        private IMapper _mapper;

        public PTWQuestionController(IPTWQuestionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public IActionResult AddQuestion([FromBody]PTWQuestionDto questionDto)
        {
            var question = _mapper.Map<PTWQuestion>(questionDto);
            try
            {
                _service.Create(question);
                return Ok("Records Added Successfully.. ");
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpPost("update/{id}")]
        public IActionResult UpdateQuestion(int id, [FromBody]PTWQuestionDto questionDto)
        {
            var question = _mapper.Map<PTWQuestion>(questionDto);
            try
            {
                _service.Update(id,question);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("delete/{id}")]
        public IActionResult DeleteQuestion(int id)
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

        [HttpPost("All")]
        public IActionResult GetAllQuestion([FromBody]JObject jobject)
        {
            var question = _service.GetAllQuestions();
            List<PTWQuestionDto> Dto = new List<PTWQuestionDto>();
            foreach (var item in question)
            {
                PTWQuestionDto Dtos = _mapper.Map<PTWQuestionDto>(item);
                Dto.Add(Dtos);
            }
            //return Ok(Dto);
            return Ok(new
            {
                items = Dto,
                totalCount = Dto.Count(),
                errorMessage = string.Empty
            });
        }


        [HttpGet("getquestion/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetQuestionByID(int id)
        {
            var question = _service.GetById(id);
            if (question == null)
                return new UnauthorizedResult();

            PTWQuestionDto questionDto = new PTWQuestionDto()
            {
                ID = question.ID,
                Questions = question.Questions
            };
            return Ok(questionDto);
        }


    }
}