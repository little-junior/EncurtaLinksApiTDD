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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UrlOriginal = table.Column<string>(type: "text", nullable: false),
                    UrlGerado = table.Column<string>(type: "text", nullable: false),
                    UltimaParteUrl = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    TempoValidadeSegundos = table.Column<int>(type: "integer", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinksEncurtados", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinksEncurtados_UltimaParteUrl",
                table: "LinksEncurtados",
                column: "UltimaParteUrl",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinksEncurtados");
        }
    }
}
