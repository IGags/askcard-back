using System.Data;
using Core.Migrations;
using Dal.Question.Models;
using Dal.QuestionTopic.Models;
using FluentMigrator;

namespace Dal.Migrations.Question;

[Migration(202310282130)]
public class Date_202310282130_AddQuestion : Migration
{
    public override void Up()
    {
        var questionTbName = nameof(QuestionDal).ToLower();
        var topicTbName = nameof(QuestionTopicDal).ToLower();

        if (!Schema.Table(topicTbName).Exists())
        {
            Create.Table(topicTbName)
                .WithColumn(nameof(QuestionTopicDal.Id)).AsPrimaryGuid()
                .WithColumn(nameof(QuestionTopicDal.TopicName)).AsString().NotNullable();
        }

        if (!Schema.Table(questionTbName).Exists())
        {
            Create.Table(questionTbName)
                .WithColumn(nameof(QuestionDal.Id)).AsPrimaryGuid()
                .WithColumn(nameof(QuestionDal.QuestionType)).AsString().NotNullable()
                .WithColumn(nameof(QuestionDal.QuestionData)).AsString().Nullable()
                .WithColumn(nameof(QuestionDal.TopicId)).AsGuid()
                .ForeignKey(topicTbName, nameof(QuestionTopicDal.Id)).OnDeleteOrUpdate(Rule.Cascade);
        }
    }

    public override void Down()
    {
        throw new System.NotImplementedException();
    }
}