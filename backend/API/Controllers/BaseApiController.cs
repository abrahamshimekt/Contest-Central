using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
        protected ActionResult HandleCommand<T>(HttpStatusCode statusCode, T payload)
        {
            if (statusCode == HttpStatusCode.Created)
            {
                return Created("", payload);
            }
            else if (statusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(payload);
            }
            else if (statusCode == HttpStatusCode.OK)
            {
                return Ok(payload);
            }
            else if (statusCode == HttpStatusCode.NotFound)
            {
                return NotFound(payload);
            }
            else if (statusCode == HttpStatusCode.Accepted)
            {
                return Accepted(payload);
            }
            else if (statusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized(payload);
            }
            return NoContent();
        }


    }
}