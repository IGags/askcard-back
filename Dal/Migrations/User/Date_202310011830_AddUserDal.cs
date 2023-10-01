using System;
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
                .WithColumn(nameof(UserDal.Id)).AsGuid().PrimaryKey()
                .WithColumn(nameof(UserDal.Login)).AsString().NotNullable()
                .WithColumn(nameof(UserDal.PasswordHash)).AsString().NotNullable();
        }
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}