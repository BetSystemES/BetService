using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BetEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BetPaidType",
                table: "Bets",
                newName: "PayoutStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PayoutStatus",
                table: "Bets",
                newName: "BetPaidType");
        }
    }
}
