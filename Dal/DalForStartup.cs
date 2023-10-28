using Dal.Question.Repositories;
using Dal.Question.Repositories.Interfaces;
using Dal.QuestionTopic.Repositories;
using Dal.QuestionTopic.Repositories.Interfaces;
using Dal.User;
using Dal.User.Interfaces;
using Dal.UserOperation;
using Dal.UserOperation.Interfaces;
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
        collection.AddScoped<IUserOperationRepository, UserOperationRepository>();
        collection.AddScoped<IQuestionRepository, QuestionRepository>();
        collection.AddScoped<IQuestionTopicRepository, QuestionTopicRepository>();

        return collection;
    }
}