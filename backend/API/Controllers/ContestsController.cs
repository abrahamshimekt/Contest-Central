using System.Net;
using Application.Features.Contests.Commands;
using Application.Features.Contests.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    public class ContestsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ContestsController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "ContestCreator")]
        [Route("CreateContest")]
        public async Task<ActionResult> CreateContest([FromBody] CreateContestDto createContestDto)
        {
            var command = new CreateContestCommand { CreateContestDto = createContestDto };
            var result = await _mediator.Send(command);
            var status = result.IsSuccess ? HttpStatusCode.Created : HttpStatusCode.BadRequest;
            return HandleCommand(status, result); 
        }
    }
}
