using System;
using Application.Common.Responses;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Groups.CQRS.Command;
using Application.Features.Groups.Dtos.Validators;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Groups.CQRS.Handler
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, CommandResponse<Unit>>
    {
                private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public UpdateGroupCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public async Task<CommandResponse<Unit>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {

            var response = new CommandResponse<Unit>();

             var validator = new UpdateGroupDtoValidator();

            var validationResult = await validator.ValidateAsync(request.UpdateGroupDto);


            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(q => q.ErrorMessage).ToList().First());

            var group = await _unitOfWork.GroupRepository.Get(request.UpdateGroupDto.Id) ?? throw new NotFoundException("Resource Not Found", request.UpdateGroupDto);
            _mapper.Map(request.UpdateGroupDto, group);

            if (await _unitOfWork.GroupRepository.Update(group) == 0)
                throw new InternalServerErrorException("Database Error: Unable To Save");


            response.IsSuccess = true;
            response.Message = "Update Successful";
            response.Data = Unit.Value;

            return response;

            
        }
    }
}