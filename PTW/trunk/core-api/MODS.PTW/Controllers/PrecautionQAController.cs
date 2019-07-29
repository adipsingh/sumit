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
    [Route("api/precaution")]
    [ApiController]
    public class PrecautionQAController : ControllerBase
    {
        private IPrecautionService _service;
        private IMapper _mapper;
        public PrecautionQAController(IPrecautionService service , IMapper mapper) { _service = service;_mapper = mapper; }

        [HttpPost("Add")]
        public IActionResult AddQuestion([FromBody]PrecautionQADto questionDto)
        {
            var question = _mapper.Map<PrecautionQA>(questionDto);
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
        public IActionResult UpdateQuestion(int id, [FromBody]PrecautionQADto questionDto)
        {
            var question = _mapper.Map<PrecautionQA>(questionDto);
            try
            {
                _service.Update(id, question);
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

        [HttpGet("All")]
        public IActionResult GetAllQuestion() //[FromBody]JObject jobject
        {
            var question = _service.GetAllQuestions();
            List<PrecautionQADto> Dto = new List<PrecautionQADto>();
            foreach (var item in question)
            {
                PrecautionQADto Dtos = _mapper.Map<PrecautionQADto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
            //return Ok(new
            //{
            //    items = Dto,
            //    totalCount = Dto.Count(),
            //    errorMessage = string.Empty
            //});
        }


        [HttpGet("getquestion/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetQuestionByID(int id)
        {
            var question = _service.GetById(id);
            if (question == null)
                return new UnauthorizedResult();

            PrecautionQADto questionDto = new PrecautionQADto()
            {
                ID = question.ID,
                PrecautionTypeID=question.PrecautionTypeID,
                Question = question.Question,
                CreatedBy=question.CreatedBy,
                CreatedOn=question.CreatedOn,
                ModifiedBy=question.ModifiedBy,
                ModifiedOn=question.ModifiedOn,
               
            };
            return Ok(questionDto);
        }

        [HttpPost("getbytype")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetQuestionByTypeID([FromBody]PrecautionQADto queDto)
        {
            var question = _service.GetAllQuestions();
            List<string> Dto = new List<string>();
            foreach (var item in question)
            {
                if (queDto.PrecautionTypeID == item.PrecautionTypeID)
                {
                    PrecautionQADto Dtos = _mapper.Map<PrecautionQADto>(item);
                    Dto.Add(Dtos.Question);
                }
            }
            return Ok(Dto);
        }


    }
}