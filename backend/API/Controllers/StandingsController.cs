using System.Net;
using Application.Features.Standings.CQRS.Command;
using Application.Features.Standings.CQRS.Query;
using Application.Features.Standings.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class StandingsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public StandingsController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetStandingFilter([FromQuery] Guid contestId, [FromQuery] string university, [FromQuery] string GroupId)
        {
            var result = await _mediator.Send(new GetStandingByFilterQuery { ContestId = contestId, University = university, GroupId = GroupId });
            var status = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NotFound;

            return HandleCommand(status, result);

        }

        [HttpGet("userStatistics")]
        public async Task<IActionResult> GetUserStatistics([FromQuery] string userHandle)
        {
            var result = await _mediator.Send(new GetUserStatisticsQuery { UserHandle = userHandle });
            var status = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            return HandleCommand(status, result);
        }
        
        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetAllStandingQuery { PageNumber = pageNumber, PageSize = pageSize });
            var status = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            return HandleCommand(status, result);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateStandingDto createStandingDto)
        {
            var result = await _mediator.Send(new CreateStandingCommand { CreateStandingDto = createStandingDto });

            var status = result.IsSuccess ? HttpStatusCode.Created : HttpStatusCode.BadRequest;
            return HandleCommand(status, result);
        }



        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStandingDto updateStandingDto)
        {
            var result = await _mediator.Send(new UpdateStandingCommand { UpdateStandingDto = updateStandingDto });

            var status = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            return HandleCommand(status, result);
        }


    }
}