using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogInPood.Migrations
{
    /// <inheritdoc />
    public partial class ff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "mileage",
                table: "Cars",
                newName: "Mileage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mileage",
                table: "Cars",
                newName: "mileage");
        }
    }
}
