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
    [Table("TeamApplicationDataPolicy")]
    public class TeamApplicationDataPolicyModel : AuditableModel
    {
        public Guid TeamId { get; set; }
        public TeamModel Team { get; set; }

        public Guid ApplicationDataPolicyId { get; set; }
        public ApplicationDataPolicyModel ApplicationDataPolicy { get; set; }
    }
}
