using Microsoft.EntityFrameworkCore.Migrations;

namespace WST.Admin.Migrations
{
    public partial class Add_Attributes_To_EM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PinCount",
                table: "ElectricLocomotive",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "ElectricLocomotive",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SectionCount",
                table: "ElectricLocomotive",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UniqueNumber",
                table: "ElectricLocomotive",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PinCount",
                table: "ElectricLocomotive");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "ElectricLocomotive");

            migrationBuilder.DropColumn(
                name: "SectionCount",
                table: "ElectricLocomotive");

            migrationBuilder.DropColumn(
                name: "UniqueNumber",
                table: "ElectricLocomotive");
        }
    }
}
