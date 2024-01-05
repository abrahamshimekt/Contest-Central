using System;
using Application.Features.Groups.CQRS.Command;
using Application.Features.Groups.Dtos;
using Application.Features.Groups.Dtos.Validators;
using Application.Features.Standings.Dtos;
using Application.Features.Authentication.Dto;
using Application.Models.Identity;
using AutoMapper;
using Domain;
using Application.Features.Contests.Dtos;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            
            #region Standing
            CreateMap<Standing, StandingDto>().ReverseMap();
            CreateMap<Standing, CreateStandingDto>().ReverseMap();
            #endregion Standing
            #region  User
            
            CreateMap<ApplicationUser,AuthResponse>();
            CreateMap<AuthRequest,ApplicationUser>();
            CreateMap<RegistrationRequest,ApplicationUser>();
            CreateMap<ApplicationUser,RegistrationResponse>();
            CreateMap<ApplicationUser,UserDetailDto>().ForMember(dest=>dest.PhotoUrl,opt=>opt.MapFrom(source=>source.Photo.Url));
            CreateMap<ApplicationUser,UserDto>();
            #endregion User

            #region Group
            CreateMap<Group, CreateGroupDto>().ReverseMap();
            CreateMap<Group, UpdateGroupDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            #endregion Group

            #region Contest
            #endregion Contest
            #region Problem
            CreateMap<Problem, ProblemDto>().ReverseMap();
            CreateMap<Contest, CreateContestDto>().ReverseMap();
            #endregion Problem

        }
        
    }
}