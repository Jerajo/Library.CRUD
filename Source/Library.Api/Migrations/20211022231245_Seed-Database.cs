using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Api.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreatedDate", "DeleteFlag", "FirstName", "LastName", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("ba4238d9-e878-4fa5-8d76-2177978315bf"), new DateTime(2021, 10, 22, 19, 12, 44, 699, DateTimeKind.Local).AddTicks(2690), null, "James", "Bond", null },
                    { new Guid("56acafb7-dcbd-42d9-923d-b05716d183f4"), new DateTime(2021, 10, 22, 19, 12, 44, 699, DateTimeKind.Local).AddTicks(2762), null, "Max", "Powers", null },
                    { new Guid("0bdb0bb9-4fe3-4c6d-aab5-c7c918b7ad25"), new DateTime(2021, 10, 22, 19, 12, 44, 699, DateTimeKind.Local).AddTicks(2766), null, "Jesus", "Cris", null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "DeleteFlag", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("861964ee-c9b8-4580-acfb-93c913590a07"), new DateTime(2021, 10, 22, 19, 12, 44, 697, DateTimeKind.Local).AddTicks(1641), null, "Historia", null },
                    { new Guid("6f928818-80e3-40c1-9dfd-a9f684f6a83b"), new DateTime(2021, 10, 22, 19, 12, 44, 697, DateTimeKind.Local).AddTicks(5279), null, "Novela", null },
                    { new Guid("36e8e3b3-7027-42e4-9bbd-b7da3e9dac6c"), new DateTime(2021, 10, 22, 19, 12, 44, 697, DateTimeKind.Local).AddTicks(5295), null, "Espiritual", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("0bdb0bb9-4fe3-4c6d-aab5-c7c918b7ad25"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("56acafb7-dcbd-42d9-923d-b05716d183f4"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ba4238d9-e878-4fa5-8d76-2177978315bf"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("36e8e3b3-7027-42e4-9bbd-b7da3e9dac6c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6f928818-80e3-40c1-9dfd-a9f684f6a83b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("861964ee-c9b8-4580-acfb-93c913590a07"));
        }
    }
}
