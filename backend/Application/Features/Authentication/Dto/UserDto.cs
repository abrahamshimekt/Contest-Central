using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Features.Authentication.Dto
{
    public class UserDto
    {
        public string Id {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Email {get;set;}
        public string GroupId {get;set;}
        public string UniversityName {get;set;}
        
    }
}