using System;
using Core.RepositoryBase.Model;

namespace Dal.QuestionTopic.Models;

public class QuestionTopicDal : DalModelBase<Guid>
{
    public string TopicName { get; set; }
}