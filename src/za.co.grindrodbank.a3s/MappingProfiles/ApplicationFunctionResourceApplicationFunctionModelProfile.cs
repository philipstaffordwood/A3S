/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System.Linq;
using za.co.grindrodbank.a3s.Models;
using AutoMapper;
using za.co.grindrodbank.a3s.A3SApiResources;

namespace za.co.grindrodbank.a3s.MappingProfiles
{
    public class ApplicationFunctionResourceApplicationFunctionModelProfile : Profile
    {
        public ApplicationFunctionResourceApplicationFunctionModelProfile()
        {
            CreateMap<ApplicationFunction, ApplicationFunctionModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uuid));
            CreateMap<ApplicationFunctionModel, ApplicationFunction>().ForMember(dest => dest.Uuid, opt => opt.MapFrom(src => src.Id))
                                                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.ApplicationFunctionPermissions.Select(fp => fp.Permission)));
        }
    }
}