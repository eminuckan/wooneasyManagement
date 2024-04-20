using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WooneasyManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _added_bucket_or_main_directory_column_to__file_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BucketOrMainDirectory",
                table: "Files",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BucketOrMainDirectory",
                table: "Files");
        }
    }
}
