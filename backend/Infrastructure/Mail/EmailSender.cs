using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Contracts.Infrastructure.Mail;
using Application.Models.Mail;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Mail
{
    public class EmailSender : IEmailSender
    {
        
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    private MimeMessage CreateEmailMessage(Email email)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("email", _emailSettings.From));
        emailMessage.To.Add(new MailboxAddress("email", email.To));
        emailMessage.Subject = email.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = email.Body };
        return emailMessage;
    }

    public async Task<CommandResponse<Email>> sendEmail(Email email)
    {
        var result = new CommandResponse<Email>();

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
            var sent = await client.SendAsync(CreateEmailMessage(email));
            result.IsSuccess = true;
        }
        catch(Exception ex)
        {
            result.IsSuccess = false;
            result.Errors.Add(ex.Message);
            
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }
        

        if(result.IsSuccess )
            result.Data = email;

        return result;
    }

       
    }
}