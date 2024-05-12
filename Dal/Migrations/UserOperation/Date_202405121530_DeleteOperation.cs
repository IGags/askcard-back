using FluentMigrator;

namespace Dal.Migrations.UserOperation;

[Migration(202405121530)]
public class Date_202405121530_DeleteOperation : Migration
{
    public override void Up()
    {
        Delete.Table("UserOperationDal".ToLower());
    }

    public override void Down()
    {
        throw new System.NotImplementedException();
    }
}