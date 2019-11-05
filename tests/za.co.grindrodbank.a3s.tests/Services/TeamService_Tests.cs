/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using za.co.grindrodbank.a3s.MappingProfiles;
using za.co.grindrodbank.a3s.Models;
using za.co.grindrodbank.a3s.Repositories;
using za.co.grindrodbank.a3s.Services;
using AutoMapper;
using NSubstitute;
using Xunit;

namespace za.co.grindrodbank.a3s.tests.Services
{
    public class TeamService_Tests
    {
        IMapper mapper;
        Guid userGuid;
        Guid teamGuid;

        TeamModel mockedTeam;

        public TeamService_Tests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TeamResourceTeamModelProfile());
                cfg.AddProfile(new UserResourceUserModelProfile());
            });

            mapper = config.CreateMapper();
            teamGuid = Guid.NewGuid();
            userGuid = Guid.NewGuid();

            mockedTeam = new TeamModel();
            mockedTeam.Name = "Test team";
            mockedTeam.Id = teamGuid;
            mockedTeam.UserTeams = new List<UserTeamModel>
            {
                new UserTeamModel
                {
                    Team = mockedTeam,
                    TeamId = teamGuid,
                    // The identity server user primary keys are stored as strings, not Guids.
                    UserId = userGuid.ToString(),
                    User = new UserModel
                    {
                        // The mapper will attempt to map the user IDs and flatten them, so it needs to be set on the mock.
                        Id = userGuid.ToString(),
                    }
                }
            };
        }

        [Fact]
        public async Task GetById_GivenGuid_ReturnsTeamResource()
        {
            var teamRepository = Substitute.For<ITeamRepository>();
            var applicationDataPolicyRepository = Substitute.For<IApplicationDataPolicyRepository>();

            teamRepository.GetByIdAsync(teamGuid, false).Returns(mockedTeam);

            var teamService = new TeamService(teamRepository, applicationDataPolicyRepository, mapper);
            var teamResource = await teamService.GetByIdAsync(teamGuid);

            Assert.True(teamResource.Name == "Test team", $"Expected team name: '{teamResource.Name}' does not equal expected value: 'Test team'");
            Assert.True(teamResource.Uuid == teamGuid, $"Expected team UUID: '{teamResource.Uuid}' does not equal expected value: '{teamGuid}'");
            Assert.True(teamResource.UserIds.First() == userGuid, $"Expected User Team User UUID: '{teamResource.UserIds.First()}' does not equal expected value: '{userGuid}'");
        }
    }
}
