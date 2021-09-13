using Microsoft.EntityFrameworkCore.Migrations;

namespace alter.treinamento.data.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Desc",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Desc",
                table: "Products",
                newName: "IX_Products_Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "Desc");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Description",
                table: "Products",
                newName: "IX_Products_Desc");
        }
    }
}
