using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Responses
{
    public class CommandResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } ="";
        public T? Data { get; set; } 
        public List<string> Errors { get; set; } = new List<string>();

        public static CommandResponse<T> Success(T data)
        {
            return new CommandResponse<T> { IsSuccess = true, Data = data };
        }
        public static CommandResponse<T> Failure(string message)
        {
            return new CommandResponse<T> { IsSuccess = false, Message = message };
        }
    }
}