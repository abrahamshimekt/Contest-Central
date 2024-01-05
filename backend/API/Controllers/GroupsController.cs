using System.Net;
using Application.Features.Groups.CQRS.Command;
using Application.Features.Groups.CQRS.Query;
using Application.Features.Groups.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GroupsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public GroupsController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

       
        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetAllGroupQuery { PageNumber = pageNumber, PageSize = pageSize });
            var status = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            return HandleCommand(status, result);
        }
        [HttpGet("id")]
        public async Task<IActionResult> Get([FromQuery] string id)
        {
            var result = await _mediator.Send(new GetGroupQuery { Id = id });
            var status = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            return HandleCommand(status, result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateGroupDto createGroupDto)
        {
            var result = await _mediator.Send(new CreateGroupCommand { CreateGroupDto = createGroupDto });

            var status = result.IsSuccess ? HttpStatusCode.Created : HttpStatusCode.BadRequest;
            return HandleCommand(status, result);
        }



        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGroupDto updateGroupDto)
        {
            var result = await _mediator.Send(new UpdateGroupCommand { UpdateGroupDto = updateGroupDto });

            var status = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            return HandleCommand(status, result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var result = await _mediator.Send(new DeleteGroupCommand { Id = id });

            var status = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            return HandleCommand(status, result);
        }


        
    }
}