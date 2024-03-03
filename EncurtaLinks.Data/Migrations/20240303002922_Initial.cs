using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EncurtaLinks.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinksEncurtados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UrlOriginal = table.Column<string>(type: "TEXT", nullable: false),
                    UrlGerado = table.Column<string>(type: "TEXT", nullable: false),
                    UltimaParteUrl = table.Column<string>(type: "TEXT", nullable: false),
                    TempoValidadeSegundos = table.Column<int>(type: "INTEGER", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinksEncurtados", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinksEncurtados");
        }
    }
}
