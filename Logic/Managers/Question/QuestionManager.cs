using System.Collections.Generic;
using System.Threading.Tasks;
using Dal.Question.Models;
using Dal.Question.Repositories.Interfaces;
using Logic.Managers.Question.Interfaces;

namespace Logic.Managers.Question;

public class QuestionManager : IQuestionManager
{
    private readonly IQuestionRepository _repository;

    public QuestionManager(IQuestionRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<QuestionDal>> GetRandomQuestionsAsync(int count)
    {
        var transaction = _repository.BeginTransaction();
        var dalList = await _repository.GetRandomQuestionListAsync(count, transaction);
        return dalList;
    }
}