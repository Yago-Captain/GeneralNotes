using FluentMigrator;

namespace MyGeneralNotes.Infrastructure.Migrations.Versions;
[Migration(DataVersions.TABLE_ROUTINE_AND_EXERCICE, "Cria as tabelas de Rotinas e Exercicios")]
public class Version002 : VersionBase
{
    public override void Up()
    {
        CreateTableRoutine();
        CreateTableExercice();
    }

    private void CreateTableRoutine()
    {
        CreateTable("Routines")
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("DayOfWeek").AsInt16().NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_Routine_User_Id", "Users", "Id");

    }

    private void CreateTableExercice()
    {
        CreateTable("Exercises")
             .WithColumn("Name").AsString(100).NotNullable()
             .WithColumn("Location").AsInt32().NotNullable()
             .WithColumn("Charge").AsDouble().NotNullable()
             .WithColumn("Repetitions").AsInt32().NotNullable()
             .WithColumn("RestTime").AsInt32().NotNullable()
             .WithColumn("Equipment").AsString(100).NotNullable()
             .WithColumn("Details").AsString(100).Nullable()
             .WithColumn("RoutineId").AsInt64().NotNullable().ForeignKey("FK_Exercise_Routine_Id", "Routines", "Id").OnDeleteOrUpdate(System.Data.Rule.Cascade); ;
    }
}
