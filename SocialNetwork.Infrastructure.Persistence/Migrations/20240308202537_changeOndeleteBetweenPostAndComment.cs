using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetwork.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeOndeleteBetweenPostAndComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Publicaciones_PostId",
                table: "Comentarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Publicaciones_PostId",
                table: "Comentarios",
                column: "PostId",
                principalTable: "Publicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Publicaciones_PostId",
                table: "Comentarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Publicaciones_PostId",
                table: "Comentarios",
                column: "PostId",
                principalTable: "Publicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
