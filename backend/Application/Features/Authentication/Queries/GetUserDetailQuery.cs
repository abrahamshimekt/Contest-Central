using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Contracts.Identity;
using Application.Features.Authentication.Dto;
using MediatR;
using Microsoft.VisualBasic;

namespace Application.Features.Authentication.Queries
{
    
    public class GetUserDetailQuery : IRequest<CommandResponse<UserDetailDto>>
    {
        
        public string UserId { get;set;}
        
    }
}