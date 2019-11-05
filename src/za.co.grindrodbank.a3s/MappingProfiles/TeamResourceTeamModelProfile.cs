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
    public class TeamResourceTeamModelProfile : Profile
    {
        public TeamResourceTeamModelProfile()
        {
            CreateMap<Team, TeamModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uuid));
            CreateMap<TeamModel, Team>().ForMember(dest => dest.Uuid, opt => opt.MapFrom(src => src.Id))
                                        .ForMember(dest => dest.TeamIds, opt => opt.MapFrom(src => src.ChildTeams.Select(ct => ct.ChildTeam.Id)))
                                        .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => src.UserTeams.Select(ut => ut.User.Id)))
                                        .ForMember(dest => dest.DataPolicies, opt => opt.MapFrom(src => src.ApplicationDataPolicies.Select(tdp => tdp.ApplicationDataPolicy.Id)));
        }
    }
}
