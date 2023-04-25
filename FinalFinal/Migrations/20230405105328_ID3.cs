using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalFinal.Migrations
{
    /// <inheritdoc />
    public partial class ID3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ZakladatelId",
                table: "Kontakt",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kontakt_ZakladatelId",
                table: "Kontakt",
                column: "ZakladatelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kontakt_AspNetUsers_ZakladatelId",
                table: "Kontakt",
                column: "ZakladatelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kontakt_AspNetUsers_ZakladatelId",
                table: "Kontakt");

            migrationBuilder.DropIndex(
                name: "IX_Kontakt_ZakladatelId",
                table: "Kontakt");

            migrationBuilder.AlterColumn<string>(
                name: "ZakladatelId",
                table: "Kontakt",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
