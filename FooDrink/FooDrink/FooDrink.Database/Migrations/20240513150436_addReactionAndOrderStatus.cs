using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FooDrink.Database.Migrations
{
    public partial class addReactionAndOrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.AddColumn<int>(
                name: "Reaction",
                table: "Reviews",
                type: "int",
                nullable: true,
                defaultValue: 0);

            _ = migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropColumn(
                name: "Reaction",
                table: "Reviews");

            _ = migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");
        }
    }
}
