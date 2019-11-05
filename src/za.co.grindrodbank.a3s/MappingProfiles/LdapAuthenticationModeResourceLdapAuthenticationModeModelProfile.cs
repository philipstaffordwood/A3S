/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using za.co.grindrodbank.a3s.Models;
using AutoMapper;
using za.co.grindrodbank.a3s.A3SApiResources;

namespace za.co.grindrodbank.a3s.MappingProfiles
{
    public class LdapAuthenticationModeResourceLdapAuthenticationModeModelProfile : Profile
    {
        public LdapAuthenticationModeResourceLdapAuthenticationModeModelProfile()
        {
            CreateMap<LdapAuthenticationMode, LdapAuthenticationModeModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uuid));
            CreateMap<LdapAuthenticationModeModel, LdapAuthenticationMode>().ForMember(dest => dest.Uuid, opt => opt.MapFrom(src => src.Id));
        }
    }
}
