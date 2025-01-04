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
            CreateMap<CreateCourseDto, Course>().ReverseMap();
            CreateMap<UserAppDto, UserApp>().ReverseMap();

            CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));

            CreateMap<OrderDetail, OrderDetailDto>()
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Course.Price));
        }
    }
}