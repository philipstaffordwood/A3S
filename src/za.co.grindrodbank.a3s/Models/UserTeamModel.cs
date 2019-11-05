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
    [Table("UserTeam")]
    public class UserTeamModel : AuditableModel
    {
        public string UserId { get; set; }
        public UserModel User { get; set; }

        public Guid TeamId { get; set; }
        public TeamModel Team { get; set; }
    }
}
