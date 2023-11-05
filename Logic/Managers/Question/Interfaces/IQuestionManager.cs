using System.Collections.Generic;
using System.Threading.Tasks;
using Dal.Question.Models;

namespace Logic.Managers.Question.Interfaces;

/// <summary>
/// Менеджер вопросов
/// </summary>
public interface IQuestionManager
{
    public Task<List<QuestionDal>> GetRandomQuestionsAsync(int count);
}