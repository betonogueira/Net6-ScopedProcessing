using FluentMigrator;
using RabbitOracleWorker.Entities;

namespace RabbitOracleWorker.Infrastructure.Migrations;

[Migration(202205160001)]
public class InitialTables_202205160001 : Migration
{
    public override void Down()
    {
        Delete.FromTable("Clientes")
            .Row(new Cliente
            {
                Id = new Guid("59c0d403-71ce-4ac8-9c2c-b0e54e7c043b").ToString(),
                Name = "Test Employee",
                Email = "beto@beto.com"
            });

        Delete.Table("Clientes");
    }
    public override void Up()
    {
        Create.Table("Clientes")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("Email").AsString(120).NotNullable();

        Insert.IntoTable("Clientes")
            .Row(new Cliente
            {
                Id = new Guid("59c0d403-71ce-4ac8-9c2c-b0e54e7c043b").ToString(),
                Name = "Test Employee",
                Email = "beto@beto.com"
            });
    }
}

