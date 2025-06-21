using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s24196_apbd_final.Migrations
{
    /// <inheritdoc />
    public partial class ChangeConcertName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Concert",
                keyColumn: "ConcertId",
                keyValue: 1,
                column: "Name",
                value: "Concert 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Concert",
                keyColumn: "ConcertId",
                keyValue: 1,
                column: "Name",
                value: "Orange Warsaw Festival");
        }
    }
}
