using System;
using Core.Migrations;
using Dal.User;
using FluentMigrator;

namespace Dal.Migrations.User;

/// <summary>
/// Пользователи
/// </summary>
[Migration(202310011830)]
public class Date_202310011830_AddUserDal : Migration
{
    public override void Up()
    {
        var tbName = nameof(UserDal).ToLower();
        if (!Schema.Table(tbName).Exists())
        {
            Create.Table(tbName)
                .WithColumn(nameof(UserDal.Id)).AsPrimaryGuid()
                .WithColumn(nameof(UserDal.Name)).AsString().NotNullable()
                .WithColumn(nameof(UserDal.PasswordHash)).AsString().NotNullable()
                .WithColumn(nameof(UserDal.Email)).AsString().NotNullable().Unique()
                .WithColumn(nameof(UserDal.IsAgree)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserDal.Role)).AsString().NotNullable();
        }
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}