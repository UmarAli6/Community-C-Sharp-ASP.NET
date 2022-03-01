using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Community.Migrations
{
    public partial class CreateMessageSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    SenderId = table.Column<string>(nullable: false),
                    ReceiverId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
