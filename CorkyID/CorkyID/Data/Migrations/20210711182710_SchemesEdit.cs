using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CorkyID.Data.Migrations
{
    public partial class SchemesEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Schemes",
                table: "Schemes");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Schemes");

            migrationBuilder.AddColumn<Guid>(
                name: "SchemeID",
                table: "Schemes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerID",
                table: "Schemes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schemes",
                table: "Schemes",
                column: "SchemeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Schemes",
                table: "Schemes");

            migrationBuilder.DropColumn(
                name: "SchemeID",
                table: "Schemes");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Schemes");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Schemes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schemes",
                table: "Schemes",
                column: "ID");
        }
    }
}
