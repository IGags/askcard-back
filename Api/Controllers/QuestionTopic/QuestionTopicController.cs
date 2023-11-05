using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Controllers.QuestionTopic.Dto.Request;
using Api.Controllers.QuestionTopic.Dto.Response;
using Dal.Constants;
using Dal.QuestionTopic.Models;
using Dal.QuestionTopic.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.QuestionTopic;

[Route("api/v1/public/provider")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Provider)]
public class QuestionTopicController : Controller
{
    private readonly IQuestionTopicRepository _repository;

    public QuestionTopicController(IQuestionTopicRepository repository)
    {
        _repository = repository;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(CreateTopicResponse), 200)]
    public async Task<IActionResult> CreateTopicAsync([FromBody]CreateTopicRequest request)
    {
        var dal = new QuestionTopicDal()
        {
            TopicName = request.TopicName
        };
        var transaction = _repository.BeginTransaction();
        var id = await _repository.InsertAsync(dal, transaction);
        
        await transaction.CommitAsync();
        var response = new CreateTopicResponse()
        {
            Id = id
        };
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateTopicAsync([FromBody] UpdateTopicRequest request)
    {
        var dal = new QuestionTopicDal()
        {
            TopicName = request.TopicName,
            Id = request.Id.Value
        };
        var transaction = _repository.BeginTransaction();

        await _repository.UpdateAsync(dal, transaction);
        await transaction.CommitAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> DeleteTopicAsync([FromRoute, Required] Guid? id)
    {
        var transaction = _repository.BeginTransaction();
        await _repository.DeleteAsync(id.Value, transaction);
        await transaction.CommitAsync();
        return Ok();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetTopicResponse), 200)]
    public async Task<IActionResult> GetTopicAsync([FromRoute, Required] Guid? id)
    {
        var transaction = _repository.BeginTransaction();

        var dal = await _repository.GetAsync(id.Value, transaction);
        await transaction.CommitAsync();
        var response = MapGetResponse(dal);

        return Ok(response);
    }

    [HttpGet("list")]
    [ProducesResponseType(typeof(GetTopicListResponse), 200)]
    public async Task<IActionResult> GetTopicListAsync()
    {
        var transaction = _repository.BeginTransaction();
        var dalList = await _repository.GetAllAsync(transaction);
        await transaction.CommitAsync();

        var responseList = dalList.Select(MapGetResponse).ToList();

        var response = new GetTopicListResponse()
        {
            ResponseList = responseList
        };

        return Ok(response);
    }
    
    private GetTopicResponse MapGetResponse(QuestionTopicDal dal)
    {
        var response = new GetTopicResponse()
        {
            Id = dal.Id,
            TopicName = dal.TopicName
        };
        return response;
    }
}