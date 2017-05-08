﻿using Tnf.Architecture.Dto.Helpers;
using Tnf.Domain.Values;

namespace Tnf.Architecture.Dto.ValueObjects
{
    public class ZipCode : ValueObject<ZipCode>
    {
        public const int Length = 8;
        public string Number { get; private set; }

        public ZipCode(string number)
        {
            SetNumber(number);
        }

        public void SetNumber(string number)
        {
            Number = ClearZipCode(number);
        }

        public static string ClearZipCode(string zipCode)
        {
            zipCode = TextHelper.GetNumbers(zipCode);

            if (string.IsNullOrEmpty(zipCode))
                return string.Empty;

            while (zipCode.StartsWith("0"))
                zipCode = zipCode.Substring(1);

            return zipCode;
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Number))
                return string.Empty;

            while (Number.Length < Length)
                Number = "0" + Number;

            return Number.Substring(0, 5) + "-" + Number.Substring(5);
        }
    }
}