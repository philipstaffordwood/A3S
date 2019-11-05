/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
namespace za.co.grindrodbank.a3s.Services
{
    public interface ISafeRandomizerService
    {
        Int32 Next(Int32 minValue, Int32 maxValue);
        string RandomString(Int32 maxLength);
    }
}
