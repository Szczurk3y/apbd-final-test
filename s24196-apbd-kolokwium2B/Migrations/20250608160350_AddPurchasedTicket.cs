using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace s24196_apbd_final.Migrations
{
    /// <inheritdoc />
    public partial class AddPurchasedTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Purchased_Ticket",
                columns: new[] { "CustomerId", "TicketConcertId", "PurchasedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Purchased_Ticket",
                keyColumns: new[] { "CustomerId", "TicketConcertId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Purchased_Ticket",
                keyColumns: new[] { "CustomerId", "TicketConcertId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Purchased_Ticket",
                keyColumns: new[] { "CustomerId", "TicketConcertId" },
                keyValues: new object[] { 3, 3 });
        }
    }
}
