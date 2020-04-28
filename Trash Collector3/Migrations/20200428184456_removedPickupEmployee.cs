using Microsoft.EntityFrameworkCore.Migrations;

namespace Trash_Collector3.Migrations
{
    public partial class removedPickupEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2622528f-32cb-4556-a5ac-3ef9fffe109f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31468d94-9b6c-4a43-83e1-8a6d0097c58c");

            migrationBuilder.DropColumn(
                name: "PickUpDay",
                table: "Employees");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "105e8f25-76fd-46ee-8ec1-aae77154dfd7", "15801a45-766e-4991-accb-fd41bc5dc4bd", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a3e1587f-358d-4065-8769-7b27e877b3e5", "3be6e4dd-eefa-43ae-9905-075bb18a5a0f", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "105e8f25-76fd-46ee-8ec1-aae77154dfd7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3e1587f-358d-4065-8769-7b27e877b3e5");

            migrationBuilder.AddColumn<int>(
                name: "PickUpDay",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "31468d94-9b6c-4a43-83e1-8a6d0097c58c", "0db3a488-be7c-435d-a6bf-e85d81abaaed", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2622528f-32cb-4556-a5ac-3ef9fffe109f", "e44a2f01-a849-4e6a-99bd-afaca6529e7e", "Employee", "EMPLOYEE" });
        }
    }
}
