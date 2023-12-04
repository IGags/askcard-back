using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Controllers.QuestionClient.Dto.Response;
using Core.Validation.Attributes;
using Dal.Constants;
using Dal.Question.Models;
using Dal.Question.Repositories.Interfaces;
using Logic.Managers.Question.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.QuestionClient;

[Route("api/v1/public/client/question")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Client)]
public class QuestionClientController : Controller
{
    private readonly IQuestionManager _questionManager;

    public QuestionClientController(IQuestionManager questionManager)
    {
        _questionManager = questionManager;
    }
    
    [HttpGet("list/{count}")]
    public async Task<IActionResult> GetRandomQuestionListAsync([FromRoute, Required, MinValue(1)]int? count, [FromQuery, IsNotEmptyGuid]Guid topicId)
    {
        var dalList = await _questionManager.GetRandomQuestionsAsync(count.Value, topicId);
        var responseList = dalList.Select(MapQuestionResponse).ToList();
        var response = new GetQuestionListResponse()
        {
            QuestionList = responseList
        };

        return Ok(response);
    }

    private GetQuestionResponse MapQuestionResponse(QuestionDal questionDal)
    {
        var response = new GetQuestionResponse()
        {
            Id = questionDal.Id,
            TopicId = questionDal.TopicId,
            QuestionData = questionDal.QuestionData,
            QuestionType = questionDal.QuestionType
        };

        return response;
    }
}