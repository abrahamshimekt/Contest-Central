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
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, CommandResponse<string>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGroupCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }
        public async Task<CommandResponse<string>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponse<string>();

            var validator = new CreateGroupDtoValidator();

            var validationResult = await validator.ValidateAsync(request.CreateGroupDto);


            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(q => q.ErrorMessage).ToList().First());

            var group = _mapper.Map<Group>(request.CreateGroupDto);

            await _unitOfWork.GroupRepository.Add(group);

            if (await _unitOfWork.CommitAsync() == 0) throw new InternalServerErrorException("Database Error: Unable to save Group");

            response.IsSuccess = true;
            response.Data = group.Id;
            response.Message = "Group Creation Successful";

            return response;








        }

    }
}