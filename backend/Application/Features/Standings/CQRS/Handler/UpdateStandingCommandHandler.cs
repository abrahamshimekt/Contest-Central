using System;
using Application.Common.Responses;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Standings.CQRS.Command;
using Application.Features.Standings.Dtos.Validators;
using AutoMapper;
using MediatR;

namespace Application.Features.Standings.CQRS.Handler
{
    public class UpdateStandingCommandHandler : IRequestHandler<UpdateStandingCommand, CommandResponse<Unit>>
    {
         private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateStandingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<CommandResponse<Unit>> Handle(UpdateStandingCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponse<Unit>();
            var validator = new UpdateStandingDtoValidator();

            var validationResult = await validator.ValidateAsync(request.UpdateStandingDto);


            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(q => q.ErrorMessage).ToList().First());

            var Standing = await _unitOfWork.StandingRepository.Get(request.UpdateStandingDto.Id) ?? throw new NotFoundException("Resource Not Found", request.UpdateStandingDto);
            _mapper.Map(request.UpdateStandingDto, Standing);
            await _unitOfWork.StandingRepository.Update(Standing);

            if (await _unitOfWork.CommitAsync()== 0)
                throw new InternalServerErrorException("Database Error: Unable To Save");


            response.IsSuccess = true;
            response.Message = "Update Successful";
            response.Data = Unit.Value;

            return response;
            
        }
    }
}