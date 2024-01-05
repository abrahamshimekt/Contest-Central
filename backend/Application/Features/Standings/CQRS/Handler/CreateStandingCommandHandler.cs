using System;
using Application.Common.Responses;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Standings.CQRS.Command;
using Application.Features.Standings.Dtos;
using Application.Features.Standings.Dtos.Validators;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Standings.CQRS.Handler
{
    public class CreateStandingCommandHandler : IRequestHandler<CreateStandingCommand, CommandResponse<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStandingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<CommandResponse<Guid>> Handle(CreateStandingCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponse<Guid>();

            var validator = new CreateStandingDtoValidator();

            var validationResult = await validator.ValidateAsync(request.CreateStandingDto);


            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(q => q.ErrorMessage).ToList().First());

            var standing = _mapper.Map<Standing>(request.CreateStandingDto);

            await _unitOfWork.StandingRepository.Add(standing);
            if ((await _unitOfWork.CommitAsync()) == 0)
                throw new InternalServerErrorException("Unable To Save Database Error");


            response.IsSuccess = true;
            response.Message = "Creation Succesful";
            response.Data = standing.Id;


            return response;

        }
    }
}