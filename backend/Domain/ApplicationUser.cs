using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public string? PhonenNumber { get; set;}
        public string GroupId { get; set;}
        public Group Group {get;set;}
        public int? AccademicYear { get; set;}
        public string UniversityName { get; set;}
        public string? PhotoId { get; set; }
        public Photo? Photo { get; set; }  
      
        
    }
}