using System;
using Application.Common.Responses;
using Application.Contracts.Persistence;
using Application.Features.Groups.CQRS.Query;
using Application.Features.Groups.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Features.Groups.CQRS.Handler
{
    public class GetAllGroupQueryHandler : IRequestHandler<GetAllGroupQuery, PaginatedResponse<GroupDto>>

    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllGroupQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }
        public async Task<PaginatedResponse<GroupDto>> Handle(GetAllGroupQuery request, CancellationToken cancellationToken)
        {
            var response = new PaginatedResponse<GroupDto>();

            var result = await _unitOfWork.GroupRepository.GetAll(request.PageNumber, request.PageSize);

            response.IsSuccess = true;
            response.Data = _mapper.Map<IReadOnlyList<GroupDto>>(result.Data);
            response.Message = "Groups Fetch Successful";

            return response;

        }
    }
}