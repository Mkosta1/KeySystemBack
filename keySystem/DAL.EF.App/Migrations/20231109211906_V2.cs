using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.App.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Site_Key_KeyId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Site_KeyId",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "KeyId",
                table: "Site");

            migrationBuilder.CreateTable(
                name: "KeyAtSite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    KeyId = table.Column<Guid>(type: "uuid", nullable: false),
                    SiteId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyAtSite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyAtSite_Key_KeyId",
                        column: x => x.KeyId,
                        principalTable: "Key",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KeyAtSite_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyAtSite_KeyId",
                table: "KeyAtSite",
                column: "KeyId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyAtSite_SiteId",
                table: "KeyAtSite",
                column: "SiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyAtSite");

            migrationBuilder.AddColumn<Guid>(
                name: "KeyId",
                table: "Site",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Site_KeyId",
                table: "Site",
                column: "KeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Key_KeyId",
                table: "Site",
                column: "KeyId",
                principalTable: "Key",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
