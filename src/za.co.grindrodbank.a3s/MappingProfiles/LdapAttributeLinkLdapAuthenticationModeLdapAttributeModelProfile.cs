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
    public class LdapAttributeLinkLdapAuthenticationModeLdapAttributeModelProfile : Profile
    {
        public LdapAttributeLinkLdapAuthenticationModeLdapAttributeModelProfile()
        {
            CreateMap<LdapAttributeLink, LdapAuthenticationModeLdapAttributeModel>();
            CreateMap<LdapAuthenticationModeLdapAttributeModel, LdapAttributeLink>();
        }
    }
}
