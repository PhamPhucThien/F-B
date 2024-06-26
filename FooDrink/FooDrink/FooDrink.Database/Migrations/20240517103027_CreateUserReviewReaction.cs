﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FooDrink.Database.Migrations
{
    public partial class CreateUserReviewReaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.CreateTable(
                name: "UserReviewReactions",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsLiked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_UserReviewReactions", x => new { x.UserId, x.ReviewId });
                    _ = table.ForeignKey(
                        name: "FK_UserReviewReactions_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_UserReviewReactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_UserReviewReactions_ReviewId",
                table: "UserReviewReactions",
                column: "ReviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropTable(
                name: "UserReviewReactions");
        }
    }
}
