using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatinumKitchen.Migrations
{
    /// <inheritdoc />
    public partial class vfd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableId",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TableId",
                table: "Orders",
                type: "int",
                nullable: true);
        }
    }
}
