using System;
using Application.Common.Responses;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Groups.CQRS.Query;
using Application.Features.Groups.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Features.Groups.CQRS.Handler
{
    public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, CommandResponse<GroupDto>>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetGroupQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;


        }
        public async Task<CommandResponse<GroupDto>> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {

            var response = new CommandResponse<GroupDto>();

            var result = await _unitOfWork.GroupRepository.Get(request.Id);

            if (result == null)
                throw new NotFoundException(nameof(result), "Group Not Found");

            response.IsSuccess = true;
            response.Data = _mapper.Map<GroupDto>(result);
            response.Message = "Group Fetch Successful";

            return response;
            




        }
    }
}