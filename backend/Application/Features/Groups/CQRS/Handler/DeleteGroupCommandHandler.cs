using System;
using Application.Common.Responses;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Groups.CQRS.Command;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Groups.CQRS.Handler
{
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, CommandResponse<Unit>>
    {
                private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public DeleteGroupCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }
        public async Task<CommandResponse<Unit>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponse<Unit>();

            var group = await _unitOfWork.GroupRepository.Get(request.Id);

            if (group == null) throw new NotFoundException(nameof(group), "Group Not Found");


            if ((await _unitOfWork.GroupRepository.Delete(group) ) == 0) throw new InternalServerErrorException("Database Error: Unable to save ");


            response.IsSuccess = true;
            response.Data = Unit.Value;
            response.Message = "Group Delete Successful";

            return response;




            
        }
    }
}