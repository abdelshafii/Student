using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Student.Migrations
{
    public partial class StartUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    DegreeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.DegreeId);
                });

            migrationBuilder.CreateTable(
                name: "governorates",
                columns: table => new
                {
                    GovernorateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_governorates", x => x.GovernorateId);
                });

            migrationBuilder.CreateTable(
                name: "institutes",
                columns: table => new
                {
                    InstituteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_institutes", x => x.InstituteId);
                });

            migrationBuilder.CreateTable(
                name: "nationalities",
                columns: table => new
                {
                    nationalityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nationalities", x => x.nationalityId);
                });

            migrationBuilder.CreateTable(
                name: "qualifications",
                columns: table => new
                {
                    qualificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qualifications", x => x.qualificationId);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    cityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GovernorateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.cityId);
                    table.ForeignKey(
                        name: "FK_Cities_governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "governorates",
                        principalColumn: "GovernorateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    departmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstituteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.departmentId);
                    table.ForeignKey(
                        name: "FK_Departments_institutes_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "institutes",
                        principalColumn: "InstituteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    frstNameArabic = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    scndNameArabic = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    thrdNameArabic = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    fristNameEnglish = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    scndNameEnglish = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    thrdNameEnglish = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    nationalityId = table.Column<int>(type: "int", nullable: false),
                    GovernorateId = table.Column<int>(type: "int", nullable: false),
                    cityId = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    qualificationId = table.Column<int>(type: "int", nullable: false),
                    DegreeId = table.Column<int>(type: "int", nullable: false),
                    percentage = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    imagurl = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    studentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteId = table.Column<int>(type: "int", nullable: false),
                    departmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Cities_cityId",
                        column: x => x.cityId,
                        principalTable: "Cities",
                        principalColumn: "cityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Degrees_DegreeId",
                        column: x => x.DegreeId,
                        principalTable: "Degrees",
                        principalColumn: "DegreeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Departments_departmentId",
                        column: x => x.departmentId,
                        principalTable: "Departments",
                        principalColumn: "departmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "governorates",
                        principalColumn: "GovernorateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_institutes_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "institutes",
                        principalColumn: "InstituteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_nationalities_nationalityId",
                        column: x => x.nationalityId,
                        principalTable: "nationalities",
                        principalColumn: "nationalityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_qualifications_qualificationId",
                        column: x => x.qualificationId,
                        principalTable: "qualifications",
                        principalColumn: "qualificationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_GovernorateId",
                table: "Cities",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InstituteId",
                table: "Departments",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_cityId",
                table: "Students",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DegreeId",
                table: "Students",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_departmentId",
                table: "Students",
                column: "departmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GovernorateId",
                table: "Students",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_InstituteId",
                table: "Students",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_nationalityId",
                table: "Students",
                column: "nationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_qualificationId",
                table: "Students",
                column: "qualificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "nationalities");

            migrationBuilder.DropTable(
                name: "qualifications");

            migrationBuilder.DropTable(
                name: "governorates");

            migrationBuilder.DropTable(
                name: "institutes");
        }
    }
}
