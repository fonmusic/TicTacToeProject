using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicTacToeAPI.Migrations
{
    /// <inheritdoc />
    public partial class TicTacToeDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Board",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "NextPlayer",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Winner",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "GameState",
                table: "Games",
                newName: "TicTacToeId");

            migrationBuilder.CreateTable(
                name: "TicTacToeDescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Board = table.Column<string>(type: "TEXT", nullable: false),
                    NextPlayer = table.Column<string>(type: "TEXT", nullable: false),
                    Winner = table.Column<string>(type: "TEXT", nullable: false),
                    GameState = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicTacToeDescription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicTacToe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DescriptionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicTacToe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicTacToe_TicTacToeDescription_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "TicTacToeDescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_TicTacToeId",
                table: "Games",
                column: "TicTacToeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicTacToe_DescriptionId",
                table: "TicTacToe",
                column: "DescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_TicTacToe_TicTacToeId",
                table: "Games",
                column: "TicTacToeId",
                principalTable: "TicTacToe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_TicTacToe_TicTacToeId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "TicTacToe");

            migrationBuilder.DropTable(
                name: "TicTacToeDescription");

            migrationBuilder.DropIndex(
                name: "IX_Games_TicTacToeId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "TicTacToeId",
                table: "Games",
                newName: "GameState");

            migrationBuilder.AddColumn<string>(
                name: "Board",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NextPlayer",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Winner",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
