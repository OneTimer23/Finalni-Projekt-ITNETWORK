using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalFinal.Migrations
{
    /// <inheritdoc />
    public partial class IdentityId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ZakladatelId",
                table: "Kontakt",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZakladatelId",
                table: "Kontakt");
        }
    }
}
