using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnDegreeAndMinDegreeToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "Degree",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hours",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinDegree",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MinDegree",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
