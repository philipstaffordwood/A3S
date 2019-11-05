/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace za.co.grindrodbank.a3s.Models
{
    [Table("UserRole")]
    public class UserRoleModel : AuditableModel
    {
        [JsonIgnore]
        public string UserId { get; set; }
        public UserModel User { get; set; }

        [JsonIgnore]
        public Guid RoleId { get; set; }
        public RoleModel Role { get; set; }
    }
}
