using System;
using FluentValidation;

namespace Application.Features.Groups.Dtos.Validators
{
    public class GroupDtoValidator : AbstractValidator<GroupDto>
    {
        public GroupDtoValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("Id is required.");
            
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");
            
        }
    }
}