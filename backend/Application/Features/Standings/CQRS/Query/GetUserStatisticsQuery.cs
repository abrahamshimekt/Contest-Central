using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Features.Standings.Dtos;
using MediatR;

namespace Application.Features.Standings.CQRS.Query
{
    public class GetUserStatisticsQuery : IRequest<CommandResponse<UserStatisticsDto>>
    {
        public string UserHandle { get; set; }
    }
}