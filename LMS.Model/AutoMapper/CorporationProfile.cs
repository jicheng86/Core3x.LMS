using AutoMapper;

using LMS.Model.Dto;
using LMS.Model.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Corporation, CorporationDto>();
            CreateMap<CorporationDto, Corporation>();
            CreateMap<CorporationDtoCreation, Corporation>();
            //CreateMap<Corporation, CorporationDto>()
            //    .ForMember(destinationMember: des => des.CorporationAddress, memberOptions: opt => opt.MapFrom(src => src.CorporationAddress));//属性字段单独映射
        }

    }
}
