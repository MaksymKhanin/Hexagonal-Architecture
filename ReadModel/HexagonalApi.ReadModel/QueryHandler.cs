// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using System;
using System.Linq;
using System.Threading.Tasks;

namespace HexagonalApi.ReadModel
{
    internal class QueryHandler
    {
        private readonly IStatusProjection _projection;

        public QueryHandler(IStatusProjection projection)
        {
            _projection = projection;
        }
        public Task<QueryStatus[]> Handle(QueryStatusRequest request)
        {
            return _projection.List(request.Id);
        }
    }

    public class QueryStatusRequest
    {
        public Guid Id { get; set; }
    }

    public class QueryStatus
    {
        public string Status { get; set; } = string.Empty;
    }

    internal class StatusDto
    {
        public string Status { get; set; } = string.Empty;
    }
}
