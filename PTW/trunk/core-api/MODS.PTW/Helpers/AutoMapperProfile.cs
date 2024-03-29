﻿using AutoMapper;
using MODS.PTW.Dtos;
using MODS.PTW.Models;

namespace MODS.PTW.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Certificate, CertificateDto>();
            CreateMap<CertificateDto, Certificate>();
            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionDto, Question>();
        }
    }
}
