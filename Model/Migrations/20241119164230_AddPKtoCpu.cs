using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class AddPKtoCpu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "CPU",
                newName: "Cpus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cpus",
                table: "Cpus",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cpus",
                table: "Cpus");

            migrationBuilder.RenameTable(
                name: "Cpus",
                newName: "CPU");
        }
    }
}
