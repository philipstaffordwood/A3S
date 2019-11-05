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
    [Table("FunctionPermission")]
    public class FunctionPermissionModel : AuditableModel
    {
        public Guid FunctionId { get; set; }
        public FunctionModel Function { get; set; }
        public Guid PermissionId { get; set; }
        public PermissionModel Permission { get; set; }
    }
}
