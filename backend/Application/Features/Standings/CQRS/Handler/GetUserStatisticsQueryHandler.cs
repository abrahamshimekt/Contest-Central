using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using Application.Common.Responses;
using Application.Contracts.Persistence;
using Application.Features.Standings.CQRS.Query;
using Application.Features.Standings.Dtos;
using MediatR;

namespace Application.Features.Standings.CQRS.Handler
{
    public class GetUserStatisticsQueryHandler : IRequestHandler<GetUserStatisticsQuery, CommandResponse<UserStatisticsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserStatisticsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandResponse<UserStatisticsDto>> Handle(GetUserStatisticsQuery request, CancellationToken cancellationToken)
        {
            var statistics =  await _unitOfWork.StandingRepository.GetUserStatistics(request.UserHandle);
            if(statistics==null){
                return CommandResponse<UserStatisticsDto>.Failure("User has no statistics");
            }

            return CommandResponse<UserStatisticsDto>.Success(statistics);
        }
    }
}