using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiFactorAuthentication.Web.Data.Migrations
{
    public partial class Credentials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Fido2Credentials",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Foo",
                table: "Fido2Credentials",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foo",
                table: "Fido2Credentials");

            migrationBuilder.AlterColumn<byte[]>(
                name: "UserId",
                table: "Fido2Credentials",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
