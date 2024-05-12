using Dal.Attempt.Repositories;
using Dal.Attempt.Repositories.Interfaces;
using Dal.Question.Repositories;
using Dal.Question.Repositories.Interfaces;
using Dal.QuestionTopic.Repositories;
using Dal.QuestionTopic.Repositories.Interfaces;
using Dal.User;
using Dal.User.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dal;

/// <summary>
/// Дал слой
/// </summary>
public static class DalForStartup
{
    public static IServiceCollection AddDal(this IServiceCollection collection)
    {
        collection.AddScoped<IUserRepository, UserRepository>();
        collection.AddScoped<IQuestionRepository, QuestionRepository>();
        collection.AddScoped<IQuestionTopicRepository, QuestionTopicRepository>();
        collection.AddScoped<IAttemptRepository, AttemptRepository>();

        return collection;
    }
}