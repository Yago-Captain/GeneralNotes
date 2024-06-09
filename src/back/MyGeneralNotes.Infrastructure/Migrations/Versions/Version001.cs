using FluentMigrator;

namespace MyGeneralNotes.Infrastructure.Migrations.Versions;

[Migration(DataVersions.TABLE_USER, "Cria a tabela de Usuarios")]
public class Version001 : VersionBase
{
    public override void Up()
    {
        CreateTable("Users")
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Email").AsString(100).NotNullable()
            .WithColumn("Password").AsString(2000).NotNullable()
            .WithColumn("UserIdentifier").AsGuid().NotNullable();
    }
}
