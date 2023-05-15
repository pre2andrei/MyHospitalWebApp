using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMyHospitalWebApp.Data.Migrations
{
    public partial class linkedUsersToPatientAndDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_user_id",
                table: "Patients",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_user_id",
                table: "Doctors",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AspNetUsers_user_id",
                table: "Doctors",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_user_id",
                table: "Patients",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AspNetUsers_user_id",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_user_id",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_user_id",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_user_id",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Doctors");
        }
    }
}
