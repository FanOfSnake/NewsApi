using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsApi.Migrations
{
    public partial class ChangedDefaultValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "Undefined Password",
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32,
                oldDefaultValue: "Some password");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "Undefined Login",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldDefaultValue: "Some login");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Undefined text",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Some cool text!");

            migrationBuilder.AlterColumn<string>(
                name: "ShortDesc",
                table: "News",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "Undefined descripton",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "Some cool descripton!");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Undefined name",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Some cool name!");

            migrationBuilder.AlterColumn<string>(
                name: "Img",
                table: "News",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "Undefined img URL",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "Some img url");

            migrationBuilder.AlterColumn<string>(
                name: "WriterName",
                table: "Comments",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "Undefined Writer",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldDefaultValue: "Some cool Writer");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Undefined comment text",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Some cool comment text!");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "Undefined name",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldDefaultValue: "Some cool name!");

            migrationBuilder.AlterColumn<string>(
                name: "Desc",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "Undefined description",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "Some cool description!");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "Some password",
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32,
                oldDefaultValue: "Undefined Password");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "Some login",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldDefaultValue: "Undefined Login");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Some cool text!",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Undefined text");

            migrationBuilder.AlterColumn<string>(
                name: "ShortDesc",
                table: "News",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "Some cool descripton!",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "Undefined descripton");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Some cool name!",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Undefined name");

            migrationBuilder.AlterColumn<string>(
                name: "Img",
                table: "News",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "Some img url",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "Undefined img URL");

            migrationBuilder.AlterColumn<string>(
                name: "WriterName",
                table: "Comments",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "Some cool Writer",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldDefaultValue: "Undefined Writer");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Some cool comment text!",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Undefined comment text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "Some cool name!",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldDefaultValue: "Undefined name");

            migrationBuilder.AlterColumn<string>(
                name: "Desc",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "Some cool description!",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "Undefined description");
        }
    }
}
