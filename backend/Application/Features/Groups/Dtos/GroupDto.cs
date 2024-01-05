using System;
using Application.Common.Dtos;

namespace Application.Features.Groups.Dtos
{
    public class GroupDto : BaseDto
    {
        public required string Name { get; set; }
        
    }
}