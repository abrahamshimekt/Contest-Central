using System;
using Application.Common.Dtos;

namespace Application.Features.Groups.Dtos
{
    public class UpdateGroupDto : BaseDto
    {

        public string Id { get; set; }
        public required string Name { get; set; }
        
    }
}