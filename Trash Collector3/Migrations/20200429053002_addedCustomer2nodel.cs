using Microsoft.EntityFrameworkCore.Migrations;

namespace Trash_Collector3.Migrations
{
    public partial class addedCustomer2nodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "105e8f25-76fd-46ee-8ec1-aae77154dfd7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3e1587f-358d-4065-8769-7b27e877b3e5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7a177f88-9758-4ac4-8989-fbc4b386ee6b", "61e91fe1-2943-4b72-bfae-412db3f451e3", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "27f2f5e7-d3b9-4b94-a5bd-7913d3d67825", "b71e31ab-5a58-4357-803a-031ecc251a3b", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27f2f5e7-d3b9-4b94-a5bd-7913d3d67825");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a177f88-9758-4ac4-8989-fbc4b386ee6b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "105e8f25-76fd-46ee-8ec1-aae77154dfd7", "15801a45-766e-4991-accb-fd41bc5dc4bd", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a3e1587f-358d-4065-8769-7b27e877b3e5", "3be6e4dd-eefa-43ae-9905-075bb18a5a0f", "Employee", "EMPLOYEE" });
        }
    }
}
