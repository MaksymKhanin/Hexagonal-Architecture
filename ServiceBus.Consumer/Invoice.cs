// This code is under Copyright (C) 2021 of Cegid SAS all right reserved

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Consumer
{
    public class Invoice
    {
        public double Amount { get; init; }

        public string Siret { get; init; }

        public Invoice(double amount, string siret)
        {
            if (amount <= 0)
            {
                throw new Exception("amount should be bigger than 0");
            }

            if (siret.Length != 9)
            {
                throw new Exception($"siret length should be 9 characters. Now it is {siret.Length} characters");
            }

            if (!siret.Contains("#"))
            {
                throw new Exception("siret should contain # symbol");
            }

            Amount = amount;
            Siret = siret;
        }
    }
}
