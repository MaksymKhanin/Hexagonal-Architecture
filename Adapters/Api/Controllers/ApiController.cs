// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using AutoMapper;
using HexagonalApi.Adapters.Api.Commands;
using HexagonalApi.Business.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HexagonalApi.Adapters.Api.Controllers
{
    [Route("Api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ApiController(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);


        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] SendPayloadCommandDto payload)
            => (await _mediator.Send(_mapper.Map<SendPayloadCommand>(payload))) switch
            {
                true => Ok(true),
                false => BadRequest()
            };
    }
}
