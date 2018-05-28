using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Indexing.Infra.SqLite.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    Age = table.Column<int>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 350, nullable: false),
                    Salary = table.Column<double>(nullable: true),
                    Score = table.Column<decimal>(nullable: true),
                    Street = table.Column<string>(maxLength: 250, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
