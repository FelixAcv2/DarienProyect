using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarienProyect.Solicitud.Data.Migrations
{
    public partial class changedsizefiellastname50 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Permission",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePermission",
                table: "Permission",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 26, 10, 2, 30, 291, DateTimeKind.Local).AddTicks(8068),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Permission",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePermission",
                table: "Permission",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 11, 26, 10, 2, 30, 291, DateTimeKind.Local).AddTicks(8068));
        }
    }
}
