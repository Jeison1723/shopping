using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.Migrations
{
    public partial class AddTemporalSaleEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemporalSale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    productId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporalSale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporalSale_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TemporalSale_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemporalSale_productId",
                table: "TemporalSale",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalSale_userId",
                table: "TemporalSale",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemporalSale");
        }
    }
}
