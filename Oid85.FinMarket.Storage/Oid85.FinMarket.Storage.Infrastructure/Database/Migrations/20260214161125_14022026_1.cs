using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oid85.FinMarket.Storage.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _14022026_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumerPriceIndexChangeEntities",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerPriceIndexChangeEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonetaryAggregateEntities",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    M0 = table.Column<double>(type: "double precision", nullable: true),
                    M1 = table.Column<double>(type: "double precision", nullable: true),
                    M2 = table.Column<double>(type: "double precision", nullable: true),
                    М2Х = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonetaryAggregateEntities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumerPriceIndexChangeEntities",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MonetaryAggregateEntities",
                schema: "public");
        }
    }
}
