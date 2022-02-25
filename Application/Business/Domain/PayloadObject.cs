// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalApi.Business.Domain
{
    public class PayloadObject
    {
        public double Amount { get; init; }

        public string Number { get; init; }

        public PayloadObject(double amount, string number)
        {
            if (amount <= 0)
            {
                throw new WrongPayloadAmountException("amount should be bigger than 0");
            }

            if (number.Length != 9)
            {
                throw new WrongNumberLengthException($"number length should be 9 characters. Now it is {number.Length} characters");
            }

            if (!number.Contains("#"))
            {
                throw new WrongNumberStructureException("number should contain # symbol");
            }

            Amount = amount;
            Number = number;
        }
    }
}