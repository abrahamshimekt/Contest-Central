using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Contracts.Identity;
using Application.Features.Authentication.Dto;
using Application.Features.Authentication.Queries;
using MediatR;

namespace Application.Features.Authentication.Handlers
{
    public class GetUserQueryDetailHandler : IRequestHandler<GetUserDetailQuery, CommandResponse<UserDetailDto>>
    {
        private readonly IAuthService _authService;
        public GetUserQueryDetailHandler(IAuthService authService){
            _authService = authService;
        }
        public async Task<CommandResponse<UserDetailDto>> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var response = await _authService.GetUserByIdAsync(request.UserId);
            return response;
        }
    }
}