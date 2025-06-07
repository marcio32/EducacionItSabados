using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_RolIdId",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "RolIdId",
                table: "Usuarios",
                newName: "RolId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_RolIdId",
                table: "Usuarios",
                newName: "IX_Usuarios_RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_RolId",
                table: "Usuarios",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_RolId",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "RolId",
                table: "Usuarios",
                newName: "RolIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                newName: "IX_Usuarios_RolIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_RolIdId",
                table: "Usuarios",
                column: "RolIdId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
