using System;
using System.Data;
using Core.Migrations;
using Dal.User;
using Dal.UserOperation;
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
        var tbName = nameof(UserOperationDal).ToLower();
        if (!Schema.Table(tbName).Exists())
        {
            Create.Table(tbName)
                .WithColumn(nameof(UserOperationDal.Id)).AsPrimaryGuid()
                .WithColumn(nameof(UserOperationDal.CustomData)).AsString().Nullable()
                .WithColumn(nameof(UserOperationDal.UserId)).AsGuid()
                .WithColumn(nameof(UserOperationDal.OperationName)).AsString().NotNullable()
                .WithColumn(nameof(UserOperationDal.Code)).AsString().NotNullable()
                .WithColumn(nameof(UserOperationDal.ExpirationDate)).AsDateTimeOffset().Nullable()
                .WithColumn(nameof(UserOperationDal.LeftAttempts)).AsInt32().NotNullable();
        }
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}