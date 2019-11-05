/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using za.co.grindrodbank.a3s.Services;

namespace za.co.grindrodbank.a3s.tests.Services
{
    public class SafeRandomizerService_Tests
    {
        [Fact]
        public void Next_GivenPositiveMinMax_ReturnsValueBetweenMinMax()
        {
            var min = 5;
            var max = 15;
            var safeRandomizerService = new SafeRandomizerService();
            var randomResult = safeRandomizerService.Next(min, max);

            Assert.InRange(randomResult, min, max);
        }

        [Fact]
        public void Next_GivenNegativeMinMax_ReturnsValueBetweenMinMax()
        {
            var min = -15;
            var max = -5;
            var safeRandomizerService = new SafeRandomizerService();
            var randomResult = safeRandomizerService.Next(min, max);

            Assert.InRange(randomResult, min, max);
        }

        [Fact]
        public void Next_GivenMixedMinMax_ReturnsValueBetweenMinMax()
        {
            var min = -15;
            var max = 5;
            var safeRandomizerService = new SafeRandomizerService();
            var randomResult = safeRandomizerService.Next(min, max);

            Assert.InRange(randomResult, min, max);
        }

        [Fact]
        public void Next_GivenGreaterMinThanMax_ReturnsArgumentOutOfRange()
        {
            var min = 10;
            var max = 5;
            var safeRandomizerService = new SafeRandomizerService();

            Exception testException = null;
            try
            {
                var randomResult = safeRandomizerService.Next(min, max);
            }
            catch (Exception ex)
            {
                testException = ex;
            }

            Assert.True(testException is ArgumentOutOfRangeException, "Specifying a greater min than max value should return an ArgumentOutOfRangeException");
        }

        [Fact]
        public void RandomString_GivenMaxLength_ReturnsRandomStringWithAlternatingLowerCharNumberUpperCharNumber()
        {
            var safeRandomizerService = new SafeRandomizerService();
            var randomString = safeRandomizerService.RandomString(10);

            Match match = Regex.Match(randomString, @"[a-z][0-9][A-Z][0-9][a-z][0-9][A-Z][0-9][a-z][0-9]", RegexOptions.Singleline);
            Assert.True(match.Success, "Random string should return string alternating between lower char, number, upper char and then number again");
        }
    }
}
