using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetwork.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removeTheFriendShipAndPostRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publicaciones_Amigos_FriendShipId",
                table: "Publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Publicaciones_FriendShipId",
                table: "Publicaciones");

            migrationBuilder.DropColumn(
                name: "FriendShipId",
                table: "Publicaciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FriendShipId",
                table: "Publicaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_FriendShipId",
                table: "Publicaciones",
                column: "FriendShipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publicaciones_Amigos_FriendShipId",
                table: "Publicaciones",
                column: "FriendShipId",
                principalTable: "Amigos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
