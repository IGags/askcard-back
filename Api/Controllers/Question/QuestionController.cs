using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Controllers.Question.Dto.Request;
using Api.Controllers.Question.Dto.Response;
using Dal.Constants;
using Dal.Question.Models;
using Dal.Question.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Question;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Provider)]
[Route("api/v1/public/provider/question")]
public class QuestionController : Controller
{
    private readonly IQuestionRepository _repository;

    public QuestionController(IQuestionRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateQuestionRequest), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateQuestionAsync([FromBody] CreateQuestionRequest request)
    {
        var dal = new QuestionDal()
        {
            Answers = request.Answers,
            QuestionText = request.QuestionText,
            QuestionType = request.QuestionType.Value,
            TopicId = request.TopicId.Value
        };
        var transaction = _repository.BeginTransaction();
        var id = await _repository.InsertAsync(dal, transaction);
        await transaction.CommitAsync();
        var response = new CreateQuestionResponse()
        {
            QuestionId = id
        };

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateQuestionAsync([FromBody] UpdateQuestionRequest request)
    {
        var dal = new QuestionDal()
        {
            Answers = request.Answers,
            QuestionText = request.QuestionText,
            QuestionType = request.QuestionType.Value,
            TopicId = request.TopicId.Value,
            Id = request.Id.Value
        };
        var transaction = _repository.BeginTransaction();
        await _repository.UpdateAsync(dal, transaction);
        await transaction.CommitAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteQuestionAsync([FromRoute, Required] Guid? id)
    {
        var transaction = _repository.BeginTransaction();
        await _repository.DeleteAsync(id.Value, transaction);
        await transaction.CommitAsync();
        return Ok();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetQuestionResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetQuestionAsync([FromRoute, Required] Guid? id)
    {
        var transaction = _repository.BeginTransaction();
        var dal = await _repository.GetAsync(id.Value, transaction);
        await transaction.CommitAsync();
        var response = FormResponse(dal);
        
        return Ok(response);
    }

    [HttpGet("list")]
    [ProducesResponseType(typeof(GetQuestionListResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetQuestionListAsync()
    {
        var transaction = _repository.BeginTransaction();
        var dalList = await _repository.GetAllAsync(transaction);
        await transaction.CommitAsync();

        var responseList = dalList.Select(FormResponse).ToList();

        var response = new GetQuestionListResponse()
        {
            QuestionList = responseList
        };

        return Ok(response);
    }

    private GetQuestionResponse FormResponse(QuestionDal dal)
    {
        var response = new GetQuestionResponse()
        {
            Answers = dal.Answers,
            QuestionText = dal.QuestionText,
            QuestionType = dal.QuestionType,
            TopicId = dal.TopicId,
            Id = dal.Id
        };

        return response;
    }
}