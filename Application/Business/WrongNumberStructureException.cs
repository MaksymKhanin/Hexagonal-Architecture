// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalApi.Business
{
    class WrongNumberStructureException : Exception
    {
        public WrongNumberStructureException(string message) : base(message) { }
    }
}
