using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Mudemy.Core.DTOs;
using Mudemy.Core.Models;

namespace Mudemy.Service
{
    internal class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<CourseDto, Course>().ReverseMap();
            CreateMap<UserAppDto, UserApp>().ReverseMap();
        }
    }
}