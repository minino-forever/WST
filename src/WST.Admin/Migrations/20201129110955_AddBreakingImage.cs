using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WST.Admin.Migrations
{
    public partial class AddBreakingImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BreakingImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BreakingId = table.Column<Guid>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakingImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreakingImage_Breaking_BreakingId",
                        column: x => x.BreakingId,
                        principalTable: "Breaking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BreakingImage_BreakingId",
                table: "BreakingImage",
                column: "BreakingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreakingImage");
        }
    }
}
