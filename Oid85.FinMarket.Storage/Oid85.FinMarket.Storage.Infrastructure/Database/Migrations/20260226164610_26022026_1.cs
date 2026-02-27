using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oid85.FinMarket.Storage.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _26022026_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BondCouponEntities",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    instrument_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ticker = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    coupon_date = table.Column<DateOnly>(type: "date", nullable: false),
                    coupon_number = table.Column<long>(type: "bigint", nullable: false),
                    coupon_period = table.Column<int>(type: "integer", nullable: false),
                    coupon_start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    coupon_end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    pay_one_bond = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BondCouponEntities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BondCouponEntities",
                schema: "public");
        }
    }
}
