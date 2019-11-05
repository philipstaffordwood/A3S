/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace za.co.grindrodbank.a3s.SecurityContractApiResources
{
    public class FunctionResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PermisionResource> Permissions { get; set; }
    }
}
