using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pesedjet.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PLAYER",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLAYER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PROFILE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    UrlProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastConnection = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalExp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROFILE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROFILE_PLAYER_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "PLAYER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TWO_FACTOR_AUTHENTICATOR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WasUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TWO_FACTOR_AUTHENTICATOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TWO_FACTOR_AUTHENTICATOR_PLAYER_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "PLAYER",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PLAYER_Email",
                table: "PLAYER",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PLAYER_Username",
                table: "PLAYER",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROFILE_PlayerId",
                table: "PROFILE",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TWO_FACTOR_AUTHENTICATOR_PlayerId",
                table: "TWO_FACTOR_AUTHENTICATOR",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PROFILE");

            migrationBuilder.DropTable(
                name: "TWO_FACTOR_AUTHENTICATOR");

            migrationBuilder.DropTable(
                name: "PLAYER");
        }
    }
}
