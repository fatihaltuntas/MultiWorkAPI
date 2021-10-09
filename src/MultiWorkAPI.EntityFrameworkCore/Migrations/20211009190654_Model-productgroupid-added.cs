using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiWorkAPI.Migrations
{
    public partial class Modelproductgroupidadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductGroupId",
                table: "Model",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductGroupId",
                table: "Model");
        }
    }
}
