using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class RemovedProductIdFromCooler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cooler",
                table: "Cooler");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Cooler");

            migrationBuilder.RenameTable(
                name: "Cooler",
                newName: "Coolers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Coolers",
                newName: "Cooler");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Cooler",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cooler",
                table: "Cooler",
                column: "ProductId");
        }
    }
}
