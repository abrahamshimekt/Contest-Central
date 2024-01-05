using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Contracts.Persistence;
using Application.Features.Contests.Commands;
using Application.Features.Contests.Dtos;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Contests.Handlers
{
    public class CreateContestCommandHandler : IRequestHandler<CreateContestCommand, CommandResponse<List<ProblemDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateContestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CommandResponse<List<ProblemDto>>> Handle(CreateContestCommand request, CancellationToken cancellationToken)
        {
            var problems = request.CreateContestDto.ProblemDtos;
            List<ProblemDto> existProblems = new List<ProblemDto>();
            List<ProblemDto> newProblems = new List<ProblemDto>();
            var contestId = Guid.NewGuid();

            for (var i = 0; i < problems.Count; i++)
            {

                var problemExists = await _unitOfWork.ProblemRepository.Get(problems[i].Id);
                if (problemExists != null)
                {
                    existProblems.Add(problems[i]);

                }
                else
                {

                    newProblems.Add(problems[i]);


                }
            }
            request.CreateContestDto.ProblemDtos = newProblems;

            var contest = _mapper.Map<Contest>(request.CreateContestDto);
            contest.Id = contestId;
            await _unitOfWork.ContestRepository.Add(contest);
            if (await _unitOfWork.CommitAsync() == 0)
            {
                return CommandResponse<List<ProblemDto>>.Failure("saving failed");
            }

            foreach (var problemDto in newProblems)
            {
                var problem = _mapper.Map<Problem>(problemDto);
                problem.ContestId = contestId;

                await _unitOfWork.ProblemRepository.Add(problem);
            }

            if (await _unitOfWork.CommitAsync() == 0)
            {
                return CommandResponse<List<ProblemDto>>.Failure("Problem Creation failed");
            }

            return CommandResponse<List<ProblemDto>>.Success(existProblems);

        }
    }
}