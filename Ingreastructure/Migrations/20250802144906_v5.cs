using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstudioId",
                table: "Turnos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_EstudioId",
                table: "Turnos",
                column: "EstudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Estudios_EstudioId",
                table: "Turnos",
                column: "EstudioId",
                principalTable: "Estudios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Estudios_EstudioId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_EstudioId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "EstudioId",
                table: "Turnos");
        }
    }
}
