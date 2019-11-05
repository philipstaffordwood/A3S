/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using NpgsqlTypes;

namespace za.co.grindrodbank.a3s.Models
{
    //This class adds Auditable fields for tracking changes

    public abstract class AuditableModel
    {
        public Guid ChangedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public NpgsqlRange<DateTime> SysPeriod { get; set; }
    }
}
