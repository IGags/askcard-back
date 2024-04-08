using Core.Migrations;
using Dal.Attempt.Models;
using Dal.User;
using FluentMigrator;

namespace Dal.Migrations.Attempt;

[Migration(202403312320)]
public class Date_202403312320_AddAttempt : Migration
{
    public override void Up()
    {
        var tbName = nameof(AttemptDal).ToLower();
        Create.Table(tbName)
            .WithColumn(nameof(AttemptDal.Id)).AsPrimaryGuid().NotNullable()
            .WithColumn(nameof(AttemptDal.AttemptTime)).AsTime().NotNullable()
            .WithColumn(nameof(AttemptDal.AttemptStartTime)).AsDateTime().NotNullable()
            .WithColumn(nameof(AttemptDal.UserId)).AsGuid().NotNullable()
            .WithColumn(nameof(AttemptDal.UserScore)).AsInt32().NotNullable()
            .WithColumn(nameof(AttemptDal.MaxPossibleScore)).AsInt32().NotNullable();
        Create.ForeignKey().FromTable(tbName).ForeignColumn(nameof(AttemptDal.UserId))
            .ToTable(nameof(UserDal).ToLower()).PrimaryColumn(nameof(UserDal.Id));
    }

    public override void Down()
    {
        throw new System.NotImplementedException();
    }
}