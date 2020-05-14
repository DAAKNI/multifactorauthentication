using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiFactorAuthentication.Web.Data.Migrations
{
    public partial class CredentialRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foo",
                table: "Fido2Credentials");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Fido2Credentials",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fido2Credentials_ApplicationUserId",
                table: "Fido2Credentials",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fido2Credentials_AspNetUsers_ApplicationUserId",
                table: "Fido2Credentials",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fido2Credentials_AspNetUsers_ApplicationUserId",
                table: "Fido2Credentials");

            migrationBuilder.DropIndex(
                name: "IX_Fido2Credentials_ApplicationUserId",
                table: "Fido2Credentials");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Fido2Credentials");

            migrationBuilder.AddColumn<string>(
                name: "Foo",
                table: "Fido2Credentials",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
