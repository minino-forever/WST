using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WST.Admin.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Breaking",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    RepairMethod = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breaking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElectricLocomotive",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Modification = table.Column<string>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricLocomotive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Patronym = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElectricLocomotiveBreakingProxy",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ElectricLocomotiveId = table.Column<Guid>(nullable: false),
                    BreakingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricLocomotiveBreakingProxy", x => new { x.BreakingId, x.ElectricLocomotiveId });
                    table.ForeignKey(
                        name: "FK_ElectricLocomotiveBreakingProxy_Breaking_BreakingId",
                        column: x => x.BreakingId,
                        principalTable: "Breaking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectricLocomotiveBreakingProxy_ElectricLocomotive_Electric~",
                        column: x => x.ElectricLocomotiveId,
                        principalTable: "ElectricLocomotive",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectricLocomotiveBreakingProxy_ElectricLocomotiveId",
                table: "ElectricLocomotiveBreakingProxy",
                column: "ElectricLocomotiveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectricLocomotiveBreakingProxy");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Breaking");

            migrationBuilder.DropTable(
                name: "ElectricLocomotive");
        }
    }
}
