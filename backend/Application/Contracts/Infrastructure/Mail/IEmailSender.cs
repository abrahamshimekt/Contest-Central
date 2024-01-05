using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Models.Mail;
namespace Application.Contracts.Infrastructure.Mail;
    public interface IEmailSender
    {
       Task<CommandResponse<Email>> sendEmail(Email email);
    }