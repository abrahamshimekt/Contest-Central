using System;
using Application.Common.Responses;
using Application.Contracts.Persistence;
using Application.Features.Standings.CQRS.Query;
using Application.Features.Standings.Dtos;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Standings.CQRS.Handler
{
    public class GetAllStandingQueryHandler : IRequestHandler<GetAllStandingQuery, PaginatedResponse<StandingDto>>
    {
         private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllStandingQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<PaginatedResponse<StandingDto>> Handle(GetAllStandingQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.StandingRepository.GetAll(request.PageNumber, request.PageSize);

        return new PaginatedResponse<StandingDto>(){
            Message= "Fetch Successful",
            IsSuccess = true,
            Data= _mapper.Map<IReadOnlyList<StandingDto>>(result.Data),
            Count= result.Count,
            PageNumber= request.PageNumber,
            PageSize= request.PageSize
        };
        }
    }
}