using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuitarCommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class OrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "PaymentStatus");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryStatus",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryStatus",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Orders",
                newName: "Status");
        }
    }
}
