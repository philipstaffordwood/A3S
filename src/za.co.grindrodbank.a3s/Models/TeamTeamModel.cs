/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace za.co.grindrodbank.a3s.Models
{
    [Table("TeamTeam")]
    public class TeamTeamModel : AuditableModel
    {
        public Guid ParentTeamId { get; set; }
        public TeamModel ParentTeam { get; set; }

        public Guid ChildTeamId { get; set; }
        public TeamModel ChildTeam { get; set; }
    }
}
