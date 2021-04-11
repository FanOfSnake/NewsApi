using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsApi.Migrations
{
    public partial class CreatingBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Some cool name!"),
                    Desc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "Some cool description!")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Img = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "Some img url"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Some cool name!"),
                    ShortDesc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "Some cool descripton!"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Some cool text!"),
                    TimePublication = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2003, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Some login"),
                    Password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, defaultValue: "Some password"),
                    Role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "user")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "CategoryNews",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    NewsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryNews", x => new { x.CategoriesId, x.NewsId });
                    table.ForeignKey(
                        name: "FK_CategoryNews_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryNews_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WriterName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Some cool Writer"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Some cool comment text!"),
                    TimeWrite = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2003, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    CurrNewsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_News_CurrNewsId",
                        column: x => x.CurrNewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryNews_NewsId",
                table: "CategoryNews",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CurrNewsId",
                table: "Comments",
                column: "CurrNewsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryNews");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "News");
        }
    }
}
