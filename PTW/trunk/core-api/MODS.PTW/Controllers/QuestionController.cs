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
using Newtonsoft.Json;

namespace MODS.PTW.Controllers
{
    [Route("api/question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private IQuestionService _questionService;
        private ICertificateService _certificateService;
        private IMapper _mapper;
        public QuestionController(IQuestionService questionService, ICertificateService certificateService, IMapper mapper)
        {
            _questionService = questionService;
            _certificateService = certificateService;
        }

        [HttpPost("add")]
        public IActionResult AddCertificateQuestion([FromBody]QuestionDto qADto)
        {
            var qa = _mapper.Map<Question>(qADto);
            try
            {
                var data = _questionService.Create(qa);
                return Ok(data);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("update/{id}")]
        public IActionResult UpdateCertificateQuestion(int id, [FromBody] QuestionDto qADto)
        {
            var qa = _mapper.Map<Question>(qADto);
            try
            {
                var data = _questionService.Update(qa, id);
                return Ok(data);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("all")]
        public IActionResult GetAllCertificateQuestion()
        {
            var qa = _questionService.GetCertificateQuestion();
            List<QuestionDto> Dto = new List<QuestionDto>();
            foreach (var item in qa)
            {
                QuestionDto Dtos = _mapper.Map<QuestionDto>(item);
                Dto.Add(Dtos);
            }
            Console.WriteLine(Dto);
            return Ok(Dto);
        }

        [HttpGet("get/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetCertificateQuestionByID(int id)
        {
            var qa = _questionService.GetById(id);

            QuestionDto question = new QuestionDto()
            {
                ID = qa.ID,
                QuestionTitle = qa.QuestionTitle,
                CreatedBy = qa.CreatedBy,
                CreatedOn = qa.CreatedOn,
                ModifiedBy = qa.ModifiedBy,
                ModifiedOn = qa.ModifiedOn

            };
            question.Certificates = new List<CertificateDto>();
            foreach (CertificateQuestion c in qa.CertificateQuestions)
            {
                Certificate certificate = c.Certificate;
                CertificateDto certificateDto = new CertificateDto();
                if (certificate == null)
                {
                    certificate = _certificateService.GetById(c.CertificateID);
                    certificateDto = new CertificateDto()
                    {
                        ID = certificate.ID,
                        Name = certificate.Name,
                        ParentID = certificate.ParentID,
                        CreatedBy = certificate.CreatedBy,
                        CreatedOn = certificate.CreatedOn,
                        ModifiedBy = certificate.ModifiedBy,
                        ModifiedOn = certificate.ModifiedOn,
                        Status = certificate.Status,
                        IsCertificateQA=certificate.IsCertificateQA

                    };
                }
                
                question.Certificates.Add(certificateDto);
            }
            return Ok(question);
        }

        [HttpPost("delete/{id}")]
        public IActionResult DeleteCertificateQuestion(int id)
        {
            try
            {
                _questionService.Delete(id);
                return Ok("Record Deleted Successfully..");
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}