using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMyHospitalWebApp.Data.Migrations
{
    public partial class initMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    danger_level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PIC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PIC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.id);
                    table.CheckConstraint("CK_DOB_Patient_before_present", "[DOB] < GETDATE()");
                });

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_patient = table.Column<int>(type: "int", nullable: false),
                    id_doctor = table.Column<int>(type: "int", nullable: false),
                    appointed_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    room = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_id_doctor",
                        column: x => x.id_doctor,
                        principalTable: "Doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_id_patient",
                        column: x => x.id_patient,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnostics",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_patinet = table.Column<int>(type: "int", nullable: false),
                    id_disease = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostics", x => x.id);
                    table.ForeignKey(
                        name: "FK_Diagnostics_Diseases_id_disease",
                        column: x => x.id_disease,
                        principalTable: "Diseases",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diagnostics_Patients_id_patinet",
                        column: x => x.id_patinet,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors_Specialities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_doctor = table.Column<int>(type: "int", nullable: false),
                    id_specialty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors_Specialities", x => x.id);
                    table.ForeignKey(
                        name: "FK_Doctors_Specialities_Doctors_id_doctor",
                        column: x => x.id_doctor,
                        principalTable: "Doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Specialities_Specialities_id_specialty",
                        column: x => x.id_specialty,
                        principalTable: "Specialities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Symptoms_Diseases",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_disease = table.Column<int>(type: "int", nullable: false),
                    id_symptom = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms_Diseases", x => x.id);
                    table.ForeignKey(
                        name: "FK_Symptoms_Diseases_Diseases_id_disease",
                        column: x => x.id_disease,
                        principalTable: "Diseases",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Symptoms_Diseases_Symptoms_id_symptom",
                        column: x => x.id_symptom,
                        principalTable: "Symptoms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_id_doctor_appointed_time",
                table: "Appointments",
                columns: new[] { "id_doctor", "appointed_time" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_id_patient_appointed_time",
                table: "Appointments",
                columns: new[] { "id_patient", "appointed_time" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_room_appointed_time",
                table: "Appointments",
                columns: new[] { "room", "appointed_time" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostics_id_disease",
                table: "Diagnostics",
                column: "id_disease");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostics_id_patinet",
                table: "Diagnostics",
                column: "id_patinet");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_PIC",
                table: "Doctors",
                column: "PIC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Specialities_id_doctor",
                table: "Doctors_Specialities",
                column: "id_doctor");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Specialities_id_specialty",
                table: "Doctors_Specialities",
                column: "id_specialty");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PIC",
                table: "Patients",
                column: "PIC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_Diseases_id_disease",
                table: "Symptoms_Diseases",
                column: "id_disease");

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_Diseases_id_symptom",
                table: "Symptoms_Diseases",
                column: "id_symptom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Diagnostics");

            migrationBuilder.DropTable(
                name: "Doctors_Specialities");

            migrationBuilder.DropTable(
                name: "Symptoms_Diseases");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Specialities");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Symptoms");
        }
    }
}
