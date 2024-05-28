using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FooDrink.Database.Migrations
{
    public partial class updateFieldRestaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.AddColumn<bool>(
                name: "IsRegistration",
                table: "Restaurants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropColumn(
                name: "IsRegistration",
                table: "Restaurants");
        }
    }
}
