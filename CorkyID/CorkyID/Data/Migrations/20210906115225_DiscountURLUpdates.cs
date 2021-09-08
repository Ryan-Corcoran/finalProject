using Microsoft.EntityFrameworkCore.Migrations;

namespace CorkyID.Data.Migrations
{
    public partial class DiscountURLUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoURL",
                table: "Discount",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RedirectURL",
                table: "Discount",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoURL",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "RedirectURL",
                table: "Discount");
        }
    }
}
