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
    [Table("ApplicationFunction")]
    public class ApplicationFunctionModel : AuditableModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ApplicationFunctionPermissionModel> ApplicationFunctionPermissions { get; set; }
        public ApplicationModel Application { get; set; }
    }
}
