using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITS_Library.Migrations
{
    public partial class asdsdasdadsafgfd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "Carts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Carts");
        }
    }
}
