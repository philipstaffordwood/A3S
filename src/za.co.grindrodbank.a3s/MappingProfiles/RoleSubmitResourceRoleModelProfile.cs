/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Linq;
using za.co.grindrodbank.a3s.Models;
using AutoMapper;
using za.co.grindrodbank.a3s.A3SApiResources;

namespace za.co.grindrodbank.a3s.MappingProfiles
{
    public class RoleSubmitResourceRoleModelProfile : Profile
    {
        public RoleSubmitResourceRoleModelProfile()
        {
            CreateMap<RoleModel, RoleSubmit>().ForMember(dest => dest.Uuid, opt => opt.MapFrom(src => src.Id))
                                              .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.User.Id)));
            CreateMap<RoleSubmit, RoleModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uuid));                          
        }
    }
}
