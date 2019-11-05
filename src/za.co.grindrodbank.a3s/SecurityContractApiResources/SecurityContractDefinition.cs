/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace za.co.grindrodbank.a3s.SecurityContractApiResources
{
    public class SecurityContractDefinition
    {
        public string Fullname { get; set; }
        public string Namespace { get; set;}
        public List<FunctionResource> Functions { get; set; }
    }
}