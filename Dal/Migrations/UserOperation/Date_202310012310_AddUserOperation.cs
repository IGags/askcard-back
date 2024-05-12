using System;
using Core.Migrations;
using FluentMigrator;

namespace Dal.Migrations.UserOperation;

/// <summary>
/// Добавление операций пользователя
/// </summary>
[Migration(202310012310)]
public class Date_202310012310_AddUserOperation : Migration
{
    public override void Up()
    {
        var tbName = "UserOperationDal".ToLower();
        if (!Schema.Table(tbName).Exists())
        {
            Create.Table(tbName)
                .WithColumn("Id").AsPrimaryGuid()
                .WithColumn("CustomData").AsString().Nullable()
                .WithColumn("UserId").AsGuid()
                .WithColumn("OperationName").AsString().NotNullable()
                .WithColumn("Code").AsString().NotNullable()
                .WithColumn("ExpirationDate").AsDateTimeOffset().Nullable()
                .WithColumn("LeftAttempts").AsInt32().NotNullable();
        }
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}