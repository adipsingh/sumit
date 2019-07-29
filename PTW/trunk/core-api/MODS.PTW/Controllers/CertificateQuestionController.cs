using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODS.PTW.Dtos;
using MODS.PTW.Services;

namespace MODS.PTW.Controllers
{
    [Route("api/certificateQuestionAnswer")]
    [ApiController]
    public class CertificateQuestionController : ControllerBase
    {
        private ICertificateQuestionService _service;

        public CertificateQuestionController(ICertificateQuestionService service) { _service = service; }

        [HttpGet("get/{id}")]       
        public IActionResult GetCertificateAnswer(int id)
        {
            var certificate = _service.GetCertificateQuestionAnswer(id);          
            return Ok(certificate);
        }
    }
}