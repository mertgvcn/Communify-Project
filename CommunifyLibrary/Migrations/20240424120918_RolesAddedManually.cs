using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunifyLibrary.Migrations
{
    public partial class RolesAddedManually : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into \"Roles\" values(1, 'Admin', '2024-04-24 15:15:00.362719+03', null, false)");
            migrationBuilder.Sql("insert into \"Roles\" values(2, 'User', '2024-04-24 15:15:00.362719+03', null, false)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
