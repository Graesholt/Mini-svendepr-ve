using Microsoft.EntityFrameworkCore.Migrations;

namespace Temperatur_API.Migrations
{
    public partial class addedroomtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomID",
                table: "Temperatures",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomID",
                table: "Moistures",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Temperatures_RoomID",
                table: "Temperatures",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Moistures_RoomID",
                table: "Moistures",
                column: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_Moistures_Rooms_RoomID",
                table: "Moistures",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Temperatures_Rooms_RoomID",
                table: "Temperatures",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moistures_Rooms_RoomID",
                table: "Moistures");

            migrationBuilder.DropForeignKey(
                name: "FK_Temperatures_Rooms_RoomID",
                table: "Temperatures");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Temperatures_RoomID",
                table: "Temperatures");

            migrationBuilder.DropIndex(
                name: "IX_Moistures_RoomID",
                table: "Moistures");

            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "Temperatures");

            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "Moistures");
        }
    }
}
