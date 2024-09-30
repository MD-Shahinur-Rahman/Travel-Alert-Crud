using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAlertRakibFinalProject.Migrations
{
    /// <inheritdoc />
    public partial class ng : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_State_StateID",
                table: "Locations");

            migrationBuilder.AlterColumn<int>(
                name: "StateID",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_State_StateID",
                table: "Locations",
                column: "StateID",
                principalTable: "State",
                principalColumn: "StateID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_State_StateID",
                table: "Locations");

            migrationBuilder.AlterColumn<int>(
                name: "StateID",
                table: "Locations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_State_StateID",
                table: "Locations",
                column: "StateID",
                principalTable: "State",
                principalColumn: "StateID");
        }
    }
}
