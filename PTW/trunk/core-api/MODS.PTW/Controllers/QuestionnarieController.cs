using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODS.PTW.Dtos;
using MODS.PTW.Helpers;
using MODS.PTW.Models;
using MODS.PTW.Services;

namespace MODS.PTW.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionnarieController : ControllerBase
    {
        private IQuestionnarieService _questionService;
        private IMapper _mapper;

        public QuestionnarieController(IQuestionnarieService questionService,IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("Add")]
        public IActionResult Create([FromBody]QuestionnarieDto questionnarieDto)
        {
            var question = _mapper.Map<Questionnarie>(questionnarieDto);

            try
            {
                _questionService.Create(question);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
