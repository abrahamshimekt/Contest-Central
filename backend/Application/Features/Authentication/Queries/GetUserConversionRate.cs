using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using MediatR;

namespace Application.Features.Authentication.Queries
{
    public class GetUserConversionRate : IRequest<CommandResponse<int>>
    {
        
    }
}