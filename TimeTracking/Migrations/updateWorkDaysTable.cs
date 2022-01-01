using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTracking.Migrations
{
    public partial class updateWorkDaysTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseHour",
                table: "WorkDays");

            migrationBuilder.DropColumn(
                name: "DayName",
                table: "WorkDays");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "WorkDays");

            migrationBuilder.DropColumn(
                name: "TotalHour",
                table: "WorkDays");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartAt",
                table: "WorkDays",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndAt",
                table: "WorkDays",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalMinute",
                table: "WorkDays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalMinute",
                table: "WorkDays");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartAt",
                table: "WorkDays",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndAt",
                table: "WorkDays",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "BaseHour",
                table: "WorkDays",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DayName",
                table: "WorkDays",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "WorkDays",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalHour",
                table: "WorkDays",
                type: "time",
                nullable: true);
        }
    }
}
