using System.Net;
using MediatR;


namespace API.Middlewares;

using Application.Common.Responses;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Serilog;
public class ExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try{
            await next(context);
        }
        catch (Exception ex){
            // Handle the exception and generate an appropriate response
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Log the exception
        Log.Error(exception, "An unhandled exception occurred: ");

        // Return a proper response to client
        if(exception is ValidationException){
            context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
            await context.Response.WriteAsJsonAsync<CommandResponse<Unit>>(
                new CommandResponse<Unit>{
                    IsSuccess=false,
                    Message="Request Data Validation Failed",
                    Errors=new List<string>{exception.Message}
                }
            );
        }else if(exception is NotAllowedException){
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            await context.Response.WriteAsJsonAsync<CommandResponse<Unit>>(
                new CommandResponse<Unit>{
                    IsSuccess=false,
                    Message=$"{exception.Message}",
                }
            );
        }
        else if(exception is NotFoundException){
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsJsonAsync<CommandResponse<Unit>>(
                new CommandResponse<Unit>{
                    IsSuccess=false,
                    Message="Resource Not Found",
                    Errors=new List<string>{exception.Message}
                }
            );
        }else{
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync<CommandResponse<Unit>>(
                new CommandResponse<Unit>{
                    IsSuccess=false,
                    Message="Failed to Process Request",
                    Errors=new List<string>{"Could not Process Request Internal Server Error"}
                }
            );
        }
    }
}