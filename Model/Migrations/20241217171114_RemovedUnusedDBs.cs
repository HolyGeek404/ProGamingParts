using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUnusedDBs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewProduct");

            migrationBuilder.DropTable(
                name: "News_Snapshot");

            migrationBuilder.DropTable(
                name: "PrepareOrder");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Promotions_TimeInterval");

            migrationBuilder.DropTable(
                name: "Sets_PriceRanges");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "Sets_TimeInterval");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewProduct",
                columns: table => new
                {
                    NewProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductType = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NewProdu__334960F65B231B87", x => x.NewProductId);
                });

            migrationBuilder.CreateTable(
                name: "News_Snapshot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Img_Title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nchar(150)", fixedLength: true, maxLength: 150, nullable: false),
                    Title_Content = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrepareOrder",
                columns: table => new
                {
                    PrepareOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepareOrder", x => x.PrepareOrderId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductType = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PromotionPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.PromotionId);
                });

            migrationBuilder.CreateTable(
                name: "Sets_TimeInterval",
                columns: table => new
                {
                    TimeIntervalId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<string>(type: "nchar(15)", fixedLength: true, maxLength: 15, nullable: false),
                    Year = table.Column<string>(type: "nchar(4)", fixedLength: true, maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets_Snapshot", x => x.TimeIntervalId);
                });

            migrationBuilder.CreateTable(
                name: "Promotions_TimeInterval",
                columns: table => new
                {
                    PromotionTimeIntervalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StardDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotio__A856C22703669DF0", x => x.PromotionTimeIntervalId);
                    table.ForeignKey(
                        name: "FK_Promotions_TimeInterval_Promotion",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId");
                });

            migrationBuilder.CreateTable(
                name: "Sets_PriceRanges",
                columns: table => new
                {
                    PriceRanges_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeInterval = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Team = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets_PriceRanges", x => x.PriceRanges_Id);
                    table.ForeignKey(
                        name: "FK_Sets_PriceRanges_Sets_TimeInterval",
                        column: x => x.TimeInterval,
                        principalTable: "Sets_TimeInterval",
                        principalColumn: "TimeIntervalId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_TimeInterval_PromotionId",
                table: "Promotions_TimeInterval",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_PriceRanges_TimeInterval",
                table: "Sets_PriceRanges",
                column: "TimeInterval");
        }
    }
}
