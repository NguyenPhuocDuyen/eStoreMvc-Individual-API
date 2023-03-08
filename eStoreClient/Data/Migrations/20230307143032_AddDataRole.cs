using Microsoft.EntityFrameworkCore.Migrations;

namespace eStoreClient.Data.Migrations
{
    public partial class AddDataRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "14c10112-ac2f-4e8b-b032-89ee038132e0", "663d7dab-14c3-4ffe-a86f-1c9ed7a6f78d", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ca58efc8-6228-430a-a205-e5490b9d2040", "3eb296a6-97de-42d3-b6c8-2b65956ed530", "customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14c10112-ac2f-4e8b-b032-89ee038132e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca58efc8-6228-430a-a205-e5490b9d2040");
        }
    }
}
