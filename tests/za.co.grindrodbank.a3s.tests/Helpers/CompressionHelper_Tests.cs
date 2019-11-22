/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using za.co.grindrodbank.a3s.Exceptions;
using za.co.grindrodbank.a3s.Helpers;

namespace za.co.grindrodbank.a3s.tests.Helpers
{
    public class CompressionHelper_Tests : IDisposable
    {
        private readonly string temporaryFolder;
        private readonly byte[] validFileContents;
        private readonly byte[] inValidFileContents;

        public CompressionHelper_Tests()
        {
            temporaryFolder = $"{ Path.GetTempPath()}{Path.DirectorySeparatorChar}{Guid.NewGuid()}";
            Directory.CreateDirectory(temporaryFolder);

            validFileContents = Convert.FromBase64String("H4sIAF3V110AA+3T0QqCMBQG4K73FHuB9KibQkmvIstmCtPFNo2I3j1t4E1SN6YE+27OBoftjPF7vuGq1pksMs1VV+Xcy7XezAoAYkLwUJOYviqEdm+REAcRjWlCQwKAIUgigA2GeceY1mrDVD/KhbWiY00uheBqoq9vK4oP59iX4LH+iaM83fAd4V4hG7O98upcmh1upKqZ2KMHWntC55e89/yXphaz3vE1/yGM+acBGfJPaOzyv4R0+O0DQqlvF2vP4ziO4yzjCdehEDwADAAA");
            inValidFileContents = Convert.FromBase64String("H4sIAF3V110AA+3T0QqCMBQG4K73FHuB9KibQkmvIstmCtPFNo2I3j1t4E1SN6YE+27OBoftjPF7vuGq1pksMs1VV+Xcy7XezAoAYkLwUJOYviqEdm+REAcRjWlCQwKAIUgigA2GeceY1mrDVD/KhbWiY00uheBqoq9vK4oP59iX4LH+iaM83fAd4V4hG7O98upcmh1upKqZ2KMHWntC55e89/yXphaz3vE1/yGM+acBGfJP");
        }

        public void Dispose()
        {
            if (Directory.Exists(temporaryFolder))
                Directory.Delete(temporaryFolder, true);
        }

        [Fact]
        public void ExtractTarGz_NullFileSpecified_ThrowsArgumentNullException()
        {
            // Arrange

            // Act
            Exception caughException = null;

            try
            {
                new CompressionHelper().ExtractTarGz(filename: null, null);
            }
            catch (Exception ex)
            {
                caughException = ex;
            }

            // Assert
            Assert.True(caughException is ArgumentNullException, "Null file specified must throw ArgumentNullException.");
        }

        [Fact]
        public void ExtractTarGz_WithFileSpecified_ExtractsArchive()
        {
            // Arrange
            string testFolder = $"{temporaryFolder}{Path.DirectorySeparatorChar}{Guid.NewGuid().ToString()}";
            string filePath = $"{testFolder}{Path.DirectorySeparatorChar}test_archive.tar.gz";
            string extractedFolder = $"{testFolder}{Path.DirectorySeparatorChar}terms_of_service";
            Directory.CreateDirectory(testFolder);

            File.WriteAllBytes(filePath, validFileContents);

            // Act
            new CompressionHelper().ExtractTarGz(filePath, extractedFolder);

            // Assert
            Assert.True(Directory.Exists(extractedFolder), $"Test existance of folder '{extractedFolder}'.");

            List<string> filesInFolder = new List<string>(Directory.GetFiles(extractedFolder));
            filesInFolder.Sort();
            
            Assert.True(filesInFolder.Count == 2, $"Extracted folder must contain 2 files.");
            Assert.True(string.Compare(Path.GetFileName(filesInFolder[0]), "terms_of_service.css") == 0, $"Extracted file 1 must have name of 'terms_of_service.css'.");
            Assert.True(string.Compare(Path.GetFileName(filesInFolder[1]), "terms_of_service.html") == 0, $"Extracted file 1 must have name of 'terms_of_service.html'.");
        }

        [Fact]
        public void ExtractTarGz_WithNullFolderSpecified_ThrowsArgumentNullException()
        {
            // Arrange
            string testFolder = $"{temporaryFolder}{Path.DirectorySeparatorChar}{Guid.NewGuid().ToString()}";
            string filePath = $"{testFolder}{Path.DirectorySeparatorChar}test_archive.tar.gz";
            string extractedFolder = $"{testFolder}{Path.DirectorySeparatorChar}terms_of_service";
            Directory.CreateDirectory(testFolder);

            File.WriteAllBytes(filePath, validFileContents);

            // Act
            Exception caughException = null;

            try
            {
                new CompressionHelper().ExtractTarGz(filePath, null);
            }
            catch (Exception ex)
            {
                caughException = ex;
            }

            // Assert
            Assert.True(caughException is ArgumentNullException, "Null file specified must throw ArgumentNullException.");
        }

        [Fact]
        public void ExtractTarGz_WithInvalidArchivespecified_ThrowsArchiveException()
        {
            // Arrange
            string testFolder = $"{temporaryFolder}{Path.DirectorySeparatorChar}{Guid.NewGuid().ToString()}";
            string filePath = $"{testFolder}{Path.DirectorySeparatorChar}test_archive.tar.gz";
            string extractedFolder = $"{testFolder}{Path.DirectorySeparatorChar}terms_of_service";
            Directory.CreateDirectory(testFolder);

            File.WriteAllBytes(filePath, inValidFileContents);

            // Act
            Exception caughException = null;

            try
            {
                new CompressionHelper().ExtractTarGz(filePath, extractedFolder);
            }
            catch (Exception ex)
            {
                caughException = ex;
            }

            // Assert
            Assert.True(caughException is ArchiveException, "Null file specified must throw ArgumentNullException.");
        }
    }
}
