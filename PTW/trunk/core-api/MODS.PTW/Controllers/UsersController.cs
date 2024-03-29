﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MODS.PTW.Dtos;
using MODS.PTW.Helpers;
using MODS.PTW.Models;
using MODS.PTW.Services;
using Newtonsoft.Json.Linq;

namespace MODS.PTW.Controllers
{
    
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private IUserService _userService;
        private IMapper _mapper;
        readonly string baseUri = "http://modsdevredesignapi.azurewebsites.net/";
        public UsersController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Login([FromBody]JObject data)
        {
             String uri = string.Format(baseUri+"API/user/Login");
           
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36";
            request.Referer = baseUri + "API/user/Login";
            request.Accept = "*/*";
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{\"username\":\"" +data["username"]+"\"," +
                              "\"password\":\""+data["password"]+"\"}";

                streamWriter.Write(json);
            }
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                JObject jobj = null;
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    String body = reader.ReadToEnd();
                    jobj = JObject.Parse(body);
                }
                return Ok(jobj);
            }
            catch (System.Net.WebException ex)
            {
                return Ok(ex.Response);
            }

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            try
            {
                //_userService.Create(user, userDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getuserprofile")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetUserProfile()
        {
            string authorizationHeader = HttpContext.Request.Headers.ContainsKey("Authorization") ? HttpContext.Request.Headers["Authorization"].ToString() : "";
            var tokenHandler = new JwtSecurityTokenHandler();
            if (User.Identity.IsAuthenticated)
            {
               // var user = _userService.GetById(Convert.ToInt32(User.Identity.Name));
               // if (user == null)
               //     return new UnauthorizedResult();
                //UserDto useDto = _mapper.Map<UserDto>(user);
               // UserDto userDto = new UserDto() { FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, PhoneNumber = user.PhoneNumber };
              //  userDto.Roles = new List<string>();
               /* if (user.Roles != null)
                {
                    foreach (UserRole userRole in user.Roles)
                    {
                        userDto.Roles.Add(userRole.Role.Name);
                    }
                }*/
                return Ok(/*userDto*/);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }



        [HttpGet("getallusers")]
        ///TODO- need to add this profile fields in db and api
        public IActionResult GetAllUsers()
        {
            string authorizationHeader = HttpContext.Request.Headers.ContainsKey("Authorization") ? HttpContext.Request.Headers["Authorization"].ToString() : "";
            var tokenHandler = new JwtSecurityTokenHandler();
            if (User.Identity.IsAuthenticated)
            {
                //var user = _userService.GetAllUser();
                List<UserDto> lstUseDto = new List<UserDto>();
                /*foreach (var item in user)
                {
                    UserDto useDto = _mapper.Map<UserDto>(item);
                    lstUseDto.Add(useDto);
                }*/
                return Ok(/*lstUseDto*/);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }



        [HttpPost("update/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto userdto)
        {
            var user = _mapper.Map<User>(userdto);
            try
            {
               // _userService.Update(user, id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpPost("delete/{id}")]
        public IActionResult DeleteUser(int id, [FromBody] UserDto Userdto)
        {
            var user = _mapper.Map<User>(Userdto);
            try
            {
               // _userService.Delete(id);
                return Ok("Record Deleted Successfully..");
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}