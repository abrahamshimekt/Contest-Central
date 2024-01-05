using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Models.Identity
{
    public class ValidateRegistrationRequest : AbstractValidator<RegistrationRequest>
    {
        public ValidateRegistrationRequest()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .Must(BeValidOrganizationEmail).WithMessage("Please provide an email from '@a2sv.org' domain.");
        }

        private bool BeValidOrganizationEmail(string email)
        {
            return email.EndsWith("@a2sv.org", StringComparison.OrdinalIgnoreCase);
        }
    }
}
