using Microsoft.EntityFrameworkCore.Migrations;

namespace Route.C41.MVC.DAL.Data.Migrations
{
    public partial class editGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Employees",
                newName: "gen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "gen",
                table: "Employees",
                newName: "Gender");
        }
    }
}
