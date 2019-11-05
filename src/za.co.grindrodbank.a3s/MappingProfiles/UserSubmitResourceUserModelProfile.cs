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
using System;

namespace za.co.grindrodbank.a3s.MappingProfiles
{
    public class UserSubmitResourceUserModelProfiles : Profile
    {
        public UserSubmitResourceUserModelProfiles()
        {
            CreateMap<UserModel, UserSubmit>().ForMember(dest => dest.Uuid, opt => opt.MapFrom(src => src.Id))
                                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
                                        .ForMember(dest => dest.TeamIds, opt => opt.MapFrom(src => src.UserTeams.Select(ut => ut.Team)))
                                        .ForMember(dest => dest.RoleIds, opt => opt.MapFrom(src => src.UserRoles.Select(ut => ut.Role)))
                                        .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => Convert.ToBase64String(src.Avatar)));
            CreateMap<UserSubmit, UserModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uuid))
                                              .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
                                              .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => Convert.FromBase64String(src.Avatar)));
        }
    }
}
