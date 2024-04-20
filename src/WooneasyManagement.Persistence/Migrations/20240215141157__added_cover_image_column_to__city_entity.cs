using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WooneasyManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _added_cover_image_column_to__city_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CoverImageId",
                table: "Cities",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CoverImageId",
                table: "Cities",
                column: "CoverImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Files_CoverImageId",
                table: "Cities",
                column: "CoverImageId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Files_CoverImageId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CoverImageId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CoverImageId",
                table: "Cities");
        }
    }
}
