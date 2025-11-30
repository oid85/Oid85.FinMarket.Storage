using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oid85.FinMarket.Storage.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _30112025_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "MaturityDate",
                schema: "public",
                table: "InstrumentEntities",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaturityDate",
                schema: "public",
                table: "InstrumentEntities");
        }
    }
}
