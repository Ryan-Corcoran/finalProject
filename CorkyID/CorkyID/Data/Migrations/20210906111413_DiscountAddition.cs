using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CorkyID.Data.Migrations
{
    public partial class DiscountAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    DiscountID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    RetailerName = table.Column<string>(nullable: true),
                    DiscountPercentage = table.Column<string>(nullable: true),
                    DiscountDescription = table.Column<string>(nullable: true),
                    ValidFrom = table.Column<DateTime>(nullable: false),
                    ValidTo = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.DiscountID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discount");
        }
    }
}
