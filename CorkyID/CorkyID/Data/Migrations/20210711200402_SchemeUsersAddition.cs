using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CorkyID.Data.Migrations
{
    public partial class SchemeUsersAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchemeUsers",
                columns: table => new
                {
                    AssignmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(nullable: false),
                    SchemeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemeUsers", x => x.AssignmentID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchemeUsers");
        }
    }
}
