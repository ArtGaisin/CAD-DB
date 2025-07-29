using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQLiteDB.Migrations
{
    /// <inheritdoc />
    public partial class CountryAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryCId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    country_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.country_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryCId",
                table: "Users",
                column: "CountryCId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Country_CountryCId",
                table: "Users",
                column: "CountryCId",
                principalTable: "Country",
                principalColumn: "country_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Country_CountryCId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropIndex(
                name: "IX_Users_CountryCId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CountryCId",
                table: "Users");
        }
    }
}
