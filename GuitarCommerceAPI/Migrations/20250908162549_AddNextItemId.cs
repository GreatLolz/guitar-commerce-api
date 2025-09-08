using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuitarCommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNextItemId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NextItemId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextItemId",
                table: "Carts");
        }
    }
}
