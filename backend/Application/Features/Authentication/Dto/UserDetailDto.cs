using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Features.Authentication.Dto
{
    public class UserDetailDto
    {
        public string Id{get;set;}
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public string? PhonenNumber { get; set;}
        public int? GroupId { get; set;}
        public string Email {get;set;}
        public string UserName {get;set;}
        public int AccademicYear { get; set;}
        public string UniversityName { get; set;}
        public string? PhotoUrl { get; set; }
        public ICollection<string> UserRole {get;set;} = new List<string>();
    }
}