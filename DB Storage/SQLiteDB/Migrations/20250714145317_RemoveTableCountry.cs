using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQLiteDB.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTableCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Отключаем проверку внешних ключей
            migrationBuilder.Sql("PRAGMA foreign_keys = OFF;");

            // Создаем временную таблицу Users без ссылки на Country
            migrationBuilder.Sql(@"
            CREATE TABLE Users_temp (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT,
                Age INTEGER NOT NULL,
                Position TEXT,
                IsMarried INTEGER NOT NULL               
            );
        ");

            // Копируем данные из старой таблицы (без Country_CId)
            migrationBuilder.Sql(@"
            INSERT INTO Users_temp (Id, Name, Age, Position, IsMarried)
            SELECT Id, Name, Age, Position, IsMarried FROM Users;
        ");

            // Удаляем старую таблицу Users
            migrationBuilder.Sql("DROP TABLE Users;");

            // Переименовываем временную таблицу
            migrationBuilder.Sql("ALTER TABLE Users_temp RENAME TO Users;");

            // Удаляем таблицу Country
            migrationBuilder.DropTable(
                name: "Country");

            // Включаем проверку внешних ключей обратно
            migrationBuilder.Sql("PRAGMA foreign_keys = ON;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Восстанавливаем таблицу Country
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

            // Создаем временную таблицу Users с восстановленной связью
            migrationBuilder.Sql(@"
            CREATE TABLE Users_temp (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT,
                Age INTEGER NOT NULL,
                Position TEXT,
                IsMarried INTEGER NOT NULL,
                Country_CId INTEGER,
                FOREIGN KEY (Country_CId) REFERENCES Country(country_id)
            );
        ");

            // Копируем данные обратно (с NULL для Country_CId)
            migrationBuilder.Sql(@"
            INSERT INTO Users_temp (Id, Name, Age, Position, IsMarried, Country_CId)
            SELECT Id, Name, Age, Position, IsMarried, NULL FROM Users;
        ");

            // Удаляем текущую таблицу Users
            migrationBuilder.Sql("DROP TABLE Users;");

            // Переименовываем временную таблицу
            migrationBuilder.Sql("ALTER TABLE Users_temp RENAME TO Users;");
        }
    }
}
