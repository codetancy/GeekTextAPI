using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class ChangeCardAttributesToChar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SecurityCode",
                table: "Payments",
                type: "char(3)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpirationYear",
                table: "Payments",
                type: "char(4)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpirationMonth",
                table: "Payments",
                type: "char(2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Cards_ExpirationMonth",
                table: "Payments",
                sql: "[ExpirationMonth] LIKE '[0][1-9]' OR [ExpirationMonth] LIKE '[1][0-2]'");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Cards_ExpirationYear",
                table: "Payments",
                sql: "[ExpirationYear] LIKE '[2]%[0-9]%'");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Cards_SecurityCode",
                table: "Payments",
                sql: "[SecurityCode] LIKE '%[0-9]%'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Cards_ExpirationMonth",
                table: "Payments");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Cards_ExpirationYear",
                table: "Payments");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Cards_SecurityCode",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "SecurityCode",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExpirationYear",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExpirationMonth",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(2)",
                oldNullable: true);
        }
    }
}
