// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalApi.Business.Domain
{
    public class PayloadSent
    {
        public Guid Id { get; set; }
        public QueryStatus[] Statuses { get; set; } = default!;
    }

    public class QueryStatus
    {
        public string Status { get; set; } = string.Empty;
    }
}
