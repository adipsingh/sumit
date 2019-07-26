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
    [Route("api/equipment")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private IEquipmentService _service;
        private IMapper _mapper;

        public EquipmentController(IEquipmentService service, IMapper mapper )
        {
            _service = service;
            _mapper = mapper;
        }



        [HttpPost("add")]
        public IActionResult AddEquipment([FromBody]EquipmentDto equipDto)
        {
            var equipment = _mapper.Map<Equipment>(equipDto);
            try
            {
                _service.Create(equipment);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("update/{id}")]
        public IActionResult UpdateEquipment(int id, [FromBody]EquipmentDto equipDto)
        {
            var equipment = _mapper.Map<Equipment>(equipDto);
            try
            {
                _service.Update(equipment, id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("All")]
        public IActionResult GetAllEquipments()
        {
            var equip = _service.GetAllEquipments();
            List<EquipmentDto> Dto = new List<EquipmentDto>();
            foreach (var item in equip)
            {
                EquipmentDto Dtos = _mapper.Map<EquipmentDto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
        }


        [HttpGet("getequipment/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetEquipmentByID(int id)
        {
            var equip = _service.GetById(id);
            if (equip == null)
                return new UnauthorizedResult();
            EquipmentDto equipDto = new EquipmentDto()
            {
                ID = equip.ID,
                CatagoryId = equip.CatagoryId,
                Name = equip.Name                
            };
            return Ok(equipDto);
        }


        [HttpPost("delete/{id}")]
        public IActionResult DeleteEquipment(int id)
        {
            //var equipment = _mapper.Map<Equipment>(equipDto);
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