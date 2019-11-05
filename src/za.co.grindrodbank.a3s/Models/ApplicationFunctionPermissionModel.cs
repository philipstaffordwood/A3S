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
    [Table("ApplicationFunctionPermission")]
    public class ApplicationFunctionPermissionModel : AuditableModel
    {
        public Guid ApplicationFunctionId { get; set; }
        public ApplicationFunctionModel ApplicationFunction { get; set; }
        public Guid PermissionId { get; set; }
        public PermissionModel Permission { get; set; }
    }
}
