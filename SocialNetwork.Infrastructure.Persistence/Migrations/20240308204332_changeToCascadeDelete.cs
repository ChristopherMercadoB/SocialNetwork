using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetwork.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeToCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Usuario_UserId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Publicaciones_Usuario_UserId",
                table: "Publicaciones");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Usuario_UserId",
                table: "Comentarios",
                column: "UserId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Publicaciones_Usuario_UserId",
                table: "Publicaciones",
                column: "UserId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Usuario_UserId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Publicaciones_Usuario_UserId",
                table: "Publicaciones");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Usuario_UserId",
                table: "Comentarios",
                column: "UserId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publicaciones_Usuario_UserId",
                table: "Publicaciones",
                column: "UserId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
