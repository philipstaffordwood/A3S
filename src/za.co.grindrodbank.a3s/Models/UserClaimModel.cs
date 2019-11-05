/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace za.co.grindrodbank.a3s.Models
{
    public class UserClaimModel : IdentityUserClaim<string>
    {
        [Key]
        public override int Id { get; set; }
        public override string ClaimType { get; set; }
        public override string ClaimValue { get; set; }
        // Note: Even though User ID's are GUIDs, the Identity table stores them as strings!
        [Column("UserId")]
        [JsonIgnore]
        public override string UserId { get; set; }
        [JsonIgnore]
        public UserModel User { get; set; }
    }
}
