using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Dal.Question.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.QuestionClient;

[Route("api/v1/public/client/question")]
public class QuestionClientController : Controller
{
    private readonly IQuestionRepository _repository;

    public QuestionClientController(IQuestionRepository repository)
    {
        _repository = repository;
    }
    
    /*[HttpGet("{id}")]
    public async Task<IActionResult> GetRandomQuestionListAsync([FromRoute, Required]Guid? id)
    {
        
    }*/
}