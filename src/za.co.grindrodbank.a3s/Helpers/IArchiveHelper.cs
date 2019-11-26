/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;
using System.IO;

namespace za.co.grindrodbank.a3s.Helpers
{
    public interface IArchiveHelper
    {
        /// <summary>
        /// Fetches a list of files inside a .tar.gz file.
        /// </summary>
        /// <param name="bytes">Byte array contianing the .tar.gz file contents.</param>
        /// <param name="flattenFileStructure">Return only the file names with no directory information.</param>
        /// <returns></returns>
        List<string> ReturnFilesListInTarGz(Byte[] bytes, bool flattenFileStructure);
    }
}
