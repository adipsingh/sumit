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
    [Route("api/equipmentcatagory")]
    [ApiController]
    public class EquipmentCatagoryController : ControllerBase
    {
        IEquipmentCatagoryService _service;
        IMapper _mapper;

        public EquipmentCatagoryController(IEquipmentCatagoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

        }
        
        [HttpPost("add")]
        public IActionResult AddEquipment([FromBody]EquipmentCatagoryDto catagoryDto)
        {
            var equipment = _mapper.Map<EquipmentCatagory>(catagoryDto);
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
        public IActionResult UpdateEquipment(int id ,[FromBody] EquipmentCatagoryDto catagoryDto)
        {
            var equipment = _mapper.Map<EquipmentCatagory>(catagoryDto);            
            try
            {
                _service.Update(equipment,id);
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
            List<EquipmentCatagoryDto> Dto = new List<EquipmentCatagoryDto>();
            foreach (var item in equip)
            {
                EquipmentCatagoryDto Dtos = _mapper.Map<EquipmentCatagoryDto>(item);
                Dto.Add(Dtos);
            }
               return Ok(Dto);           
        }


        [HttpGet("getequipment/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetEquipmentByID(int id)
        {
            var equip = _service.GetById(id);
            if (equip==null)
                return new UnauthorizedResult();
            EquipmentCatagoryDto equipDto = new EquipmentCatagoryDto() { ID =equip.ID, Catagory = equip.Catagory };
            return Ok(equipDto);                           
        }


        [HttpPost("delete/{id}")]
        public IActionResult DeleteEquipment(int id)
        {
            //var equipment = _mapper.Map<EquipmentCatagory>(catagoryDto);
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
