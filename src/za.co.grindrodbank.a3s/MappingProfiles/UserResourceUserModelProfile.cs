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
    public class UserResourceUserModelProfile : Profile
    {
        public UserResourceUserModelProfile()
        {
            CreateMap<UserModel, User>().ForMember(dest => dest.Uuid, opt => opt.MapFrom(src => src.Id))
                                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
                                        .ForMember(dest => dest.Teams, opt => opt.MapFrom(src => src.UserTeams.Select(ut => ut.Team)))
                                        .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ut => ut.Role)))
                                        .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => Convert.ToBase64String(src.Avatar)));
            CreateMap<User, UserModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uuid))
                                        .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => Convert.FromBase64String(src.Avatar)));
        }
    }
}
