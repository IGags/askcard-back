using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Controllers.QuestionTopic.Dto.Response;
using Api.Controllers.QuestionTopic.Helpers;
using Core.Validation.Attributes;
using Dal.Constants;
using Dal.QuestionTopic.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.QuestionTopicClient;

[Route("api/v1/public/client/question-topic")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Client)]
public class QuestionTopicClientController : Controller
{
    private readonly IQuestionTopicRepository _repository;

    public QuestionTopicClientController(IQuestionTopicRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTopicAsync([FromRoute, IsNotEmptyGuid]Guid id)
    {
        var transaction = _repository.BeginTransaction();
        var dal = await _repository.GetAsync(id, transaction);
        await transaction.CommitAsync();
        var response = dal.MapGetResponse();

        return Ok(response);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetTopicListAsync()
    {
        var transaction = _repository.BeginTransaction();
        var dalList = await _repository.GetAllAsync(transaction);
        await transaction.CommitAsync();

        var responseList = dalList.Select(x => x.MapGetResponse()).ToList();
        var response = new GetTopicListResponse()
        {
            ResponseList = responseList
        };

        return Ok(response);
    }
}