/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace za.co.grindrodbank.a3s.Models
{
    [Table("Application")]
    public class ApplicationModel : AuditableModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<FunctionModel> Functions{ get; set; }
        public List<ApplicationFunctionModel> ApplicationFunctions { get; set; }
        public List<ApplicationDataPolicyModel> ApplicationDataPolicies { get; set; }
    }
}
