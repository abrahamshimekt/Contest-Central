using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using Application.Common.Responses;
using Application.Contracts.Persistence;
using Application.Features.Standings.CQRS.Query;
using Application.Features.Standings.Dtos;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Features.Standings.CQRS.Handler
{
    public class GetStandingByFilterRequestHandler : IRequestHandler<GetStandingByFilterQuery, CommandResponse<IReadOnlyList<StandingDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetStandingByFilterRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse<IReadOnlyList<StandingDto>>> Handle(GetStandingByFilterQuery request, CancellationToken cancellationToken)
        {

            var response = new CommandResponse<IReadOnlyList<StandingDto>>();

            var standings =  await _unitOfWork.StandingRepository.GetStandingFilter(request.ContestId, request.University ?? "any", request.GroupId ?? "any");
            if (standings == null)
            {
                response.Data = null;
                response.Message = "No standings found";
                response.IsSuccess = false;
                return response;
            }
            List<StandingDto> standingDtos = new List<StandingDto>();
            foreach(var standing in standings){
                var st = new StandingDto();
                st.Id = standing.Id;
                st.UserHandle =standing.UserHandle;
                st.ContestId = standing.ContestId;
                st.SolvedProblems = standing.SolvedProblems;
                st.Rank = standing.Rank;
                standingDtos.Add(st);   

            }
            response.Message ="standings retrieved successfully";
            response.IsSuccess = true;
            response.Data = standingDtos;

            return response;
            

            
        }
    }
}