using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Controllers.Attempt.Dto.Request;
using Api.Controllers.Attempt.Dto.Response;
using Core.Controllers;
using Dal.Attempt.Models;
using Dal.Attempt.Repositories.Interfaces;
using Dal.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Attempt;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Client)]
[Route("api/v1/public/client/attempt")]
public class AttemptController : Controller
{
    private readonly IAttemptRepository _attemptRepository;

    public AttemptController(IAttemptRepository attemptRepository)
    {
        _attemptRepository = attemptRepository;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(CreateAttemptResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateAttemptAsync([FromBody]CreateAttemptRequest request)
    {
        var dal = new AttemptDal()
        {
            AttemptTime = request.AttemptTime,
            AttemptStartTime = request.AttemptStartTime,
            MaxPossibleScore = request.MaxPossibleScore,
            UserScore = request.UserScore,
            UserId = request.UserId
        };
        var transaction = _attemptRepository.BeginTransaction();
        var id = await _attemptRepository.InsertAsync(dal, transaction);
        await transaction.CommitAsync();
        var response = new CreateAttemptResponse()
        {
            Id = id
        };

        return Ok(response);
    }
    
    [HttpGet("list")]
    [ProducesResponseType(typeof(GetAttemptListResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAttemptListAsync([FromServices]IdentityUserService identityUserService)
    {
        var transaction = _attemptRepository.BeginTransaction();
        var attemptList = await _attemptRepository.GetByFieldAsync(nameof(AttemptDal.UserId),
            identityUserService.GetUserId(), transaction);
        await transaction.CommitAsync();
        var responseList = new List<GetAttemptResponse>();
        foreach (var attempt in attemptList)
        {
            var result = new GetAttemptResponse()
            {
                AttemptStartTime = attempt.AttemptStartTime,
                AttemptTime = attempt.AttemptTime,
                Id = attempt.Id,
                MaxPossibleScore = attempt.MaxPossibleScore,
                UserId = attempt.UserId,
                UserScore = attempt.UserScore
            };
            responseList.Add(result);
        }

        var response = new GetAttemptListResponse()
        {
            AttemptList = responseList
        };
        return Ok(response);
    }
}