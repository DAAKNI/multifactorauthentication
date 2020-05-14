using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiFactorAuthentication.Web.Data.Migrations
{
    public partial class CredentialRelationship4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Descriptor",
                table: "Fido2Credentials",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descriptor",
                table: "Fido2Credentials");
        }
    }
}
