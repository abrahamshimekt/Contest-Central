using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Contracts.Identity;
using Application.Features.Authentication.Dto;
using Application.Features.Authentication.Queries;
using MediatR;

namespace Application.Features.Users.Handlers
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, CommandResponse<List<UserDto>>>
    {
        private readonly IAuthService _authService;
        public GetUsersListQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<CommandResponse<List<UserDto>>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var response = await _authService.GetAllUserAsync();
            return response;
        }
    }
}