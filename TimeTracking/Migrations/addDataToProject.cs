using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTracking.Migrations
{
    public partial class addDataToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "IsActive", "ProjectName" },
                values: new object[,]
                {
                    { 1, true, "Infinity" },
                    { 2, true, "EVO" },
                    { 3, true, "Emerald" },
                    { 4, true, "SELECT.TSM" },
                    { 5, true, "Other" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
