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
    [Route("api/userauthority")]
    [ApiController]
    public class UserAuthorityController : ControllerBase
    {
        IUserAuthorityService _service;
        IMapper _mapper;

        public UserAuthorityController(IUserAuthorityService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpPost("Add")]
        public IActionResult AddUserAuthority ([FromBody]UserAuthorityDto userAuthority)
        {
            var authority = _mapper.Map<UserAuthority>(userAuthority);
            try
            {
                _service.Create(authority);
                return Ok("Records Added Successfully.. ");
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


        [HttpPost("update/{id}")]
        public IActionResult UpdateUserAuthority(int id, [FromBody] UserAuthorityDto UserAuthority)
        {
            var authority = _mapper.Map<UserAuthority>(UserAuthority);
            try
            {
                _service.Update(authority, id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("All")]
        public IActionResult GetAllUserAuthority()
        {
            var authority = _service.GetAllUserAuthority();
            List<UserAuthorityDto> Dto = new List<UserAuthorityDto>();
            foreach (var item in authority)
            {
                UserAuthorityDto Dtos = _mapper.Map<UserAuthorityDto>(item);
                Dto.Add(Dtos);
            }
            return Ok(Dto);
        }



        [HttpGet("getuserauthority/{id}")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetUserAuthorityByID(int id)
        {
            var authority = _service.GetById(id);
            if (authority == null)
                return new UnauthorizedResult();
            UserAuthorityDto authDto = new UserAuthorityDto() { ID = authority.ID, Authority = authority.Authority, JGCITCode= authority.JGCITCode };
            return Ok(authDto);
        }




        [HttpPost("delete/{id}")]
        public IActionResult DeleteUserAuthority(int id, [FromBody] UserAuthorityDto UserAuthority)
        {
            var authority = _mapper.Map<UserAuthority>(UserAuthority);
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