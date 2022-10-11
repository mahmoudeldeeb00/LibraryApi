using Microsoft.EntityFrameworkCore.Migrations;

namespace Api_Project.Migrations
{
    public partial class addfinishedtochecknbook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CheckFinished",
                table: "LibraryBooksChecked",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckFinished",
                table: "LibraryBooksChecked");
        }
    }
}
