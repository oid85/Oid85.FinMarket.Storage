using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oid85.FinMarket.Storage.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _27032026_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DividendInfoEntities",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstrumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Ticker = table.Column<string>(type: "text", nullable: false),
                    RecordDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DeclaredDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Dividend = table.Column<double>(type: "double precision", nullable: false),
                    DividendPrc = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DividendInfoEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForecastConsensusEntities",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ticker = table.Column<string>(type: "text", nullable: false),
                    InstrumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecommendationString = table.Column<string>(type: "text", nullable: false),
                    RecommendationNumber = table.Column<int>(type: "integer", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    CurrentPrice = table.Column<double>(type: "double precision", nullable: false),
                    ConsensusPrice = table.Column<double>(type: "double precision", nullable: false),
                    MinTarget = table.Column<double>(type: "double precision", nullable: false),
                    MaxTarget = table.Column<double>(type: "double precision", nullable: false),
                    PriceChange = table.Column<double>(type: "double precision", nullable: false),
                    PriceChangeRel = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForecastConsensusEntities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DividendInfoEntities",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ForecastConsensusEntities",
                schema: "public");
        }
    }
}
