using Microsoft.EntityFrameworkCore.Migrations;

namespace Trash_Collector3.Migrations
{
    public partial class addedWeekDays2Employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c61ffe5-4998-4d2b-b43c-13ad42212985");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6345969-2be4-452d-a937-04181562aa52");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "31468d94-9b6c-4a43-83e1-8a6d0097c58c", "0db3a488-be7c-435d-a6bf-e85d81abaaed", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2622528f-32cb-4556-a5ac-3ef9fffe109f", "e44a2f01-a849-4e6a-99bd-afaca6529e7e", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2622528f-32cb-4556-a5ac-3ef9fffe109f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31468d94-9b6c-4a43-83e1-8a6d0097c58c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b6345969-2be4-452d-a937-04181562aa52", "26e59df6-33c8-4ad7-a222-046d126636a0", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8c61ffe5-4998-4d2b-b43c-13ad42212985", "22e3eb67-4df7-4f6c-bb00-f52f53646dad", "Employee", "EMPLOYEE" });
        }
    }
}
