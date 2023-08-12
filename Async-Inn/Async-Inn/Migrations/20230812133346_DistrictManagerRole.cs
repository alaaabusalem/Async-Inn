using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Async_Inn.Migrations
{
    /// <inheritdoc />
    public partial class DistrictManagerRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "districtmanager", "00000000-0000-0000-0000-000000000000", "DistrictManager", "DISTRICTMANAGER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "districtmanager");
        }
    }
}
