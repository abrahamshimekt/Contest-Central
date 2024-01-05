using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Features.Authentication.Dto;
using MediatR;

namespace Application.Features.Authentication.Queries
{
    public class GetUsersListQuery :IRequest<CommandResponse<List<UserDto>>>
    {
        
    }
}