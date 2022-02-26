// This code is under Copyright (C) 2021 of Cegid SAS all right reserved

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Consumer
{
    public class Payload
    {
        public double Amount { get; init; }

        public string Number { get; init; }

        public Payload(double amount, string number)
        {
            if (amount <= 0)
            {
                throw new Exception("amount should be bigger than 0");
            }

            if (number.Length != 9)
            {
                throw new Exception($"number length should be 9 characters. Now it is {number.Length} characters");
            }

            if (!number.Contains("#"))
            {
                throw new Exception("number should contain # symbol");
            }

            Amount = amount;
            Number = number;
        }
    }
}
