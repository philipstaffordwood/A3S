/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace za.co.grindrodbank.a3s.Services
{
    public class SafeRandomizerService : ISafeRandomizerService
    {
        private const string ALPHA_CHARS = "abcdefghijklmnopqrstuvwxyz";
        private const string CAPITAL_ALPHA_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string DIGIT_CHARS = "1234567890";

        private readonly RandomNumberGenerator randomizer;
        private readonly byte[] uint32Buffer = new byte[4];

        public SafeRandomizerService()
        {
            randomizer = RandomNumberGenerator.Create();
        }

        public Int32 Next(Int32 minValue, Int32 maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue));

            if (minValue == maxValue) return minValue;

            Int64 diff = maxValue - minValue;
            while (true)
            {
                randomizer.GetBytes(uint32Buffer);
                var rand = BitConverter.ToUInt32(uint32Buffer, 0);

                var max = (1 + (Int64)UInt32.MaxValue);
                var remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (Int32)(minValue + (rand % diff));
                }
            }
        }

        public string RandomString(Int32 maxLength)
        {
            var randomValueSb = new StringBuilder();
            bool getNum = false;
            bool getCaps = false;

            for (int i = 0; i < maxLength; i++)
            {
                if (getNum)
                {
                    getNum = false;
                    randomValueSb.Append(DIGIT_CHARS[Next(0, DIGIT_CHARS.Length - 1)]);
                }
                else
                {
                    getNum = true;

                    if (getCaps)
                    {
                        getCaps = false;
                        randomValueSb.Append(CAPITAL_ALPHA_CHARS[Next(0, CAPITAL_ALPHA_CHARS.Length - 1)]);
                    }
                    else
                    {
                        getCaps = true;
                        randomValueSb.Append(ALPHA_CHARS[Next(0, ALPHA_CHARS.Length - 1)]);
                    }
                }
            }

            return randomValueSb.ToString();
        }
    }
}
