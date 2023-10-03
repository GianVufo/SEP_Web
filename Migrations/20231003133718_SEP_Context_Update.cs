using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEP_Web.Migrations
{
    public partial class SEP_Context_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "Evaluators",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "Evaluators",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "Evaluators");

            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "Evaluators");
        }
    }
}
