using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingCompanyWebApp.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Location = table.Column<string>(nullable: false),
                    Facebook = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteState",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    SystemOff = table.Column<bool>(nullable: false),
                    SystemOffMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    DataFile = table.Column<byte[]>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    RegDate = table.Column<DateTime>(nullable: false),
                    Specialization = table.Column<string>(nullable: false),
                    Facebook = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCompany",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Picture = table.Column<byte[]>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Specialization = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Picture = table.Column<byte[]>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    RegDate = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    CourseFee = table.Column<float>(nullable: false),
                    HourNum = table.Column<int>(nullable: false),
                    Seate = table.Column<int>(nullable: false),
                    TrainerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "CreatedDate", "Email", "Facebook", "Location", "ModifiedDate", "Phone" },
                values: new object[] { new Guid("4fb9b4ed-7871-4449-b6d7-94c51c6e6c7a"), new DateTime(2020, 10, 30, 0, 12, 34, 634, DateTimeKind.Local).AddTicks(1786), "NajlaZwawi@gmail.com", "Najla_zwawi", "Tripoli - Libya", null, "002191234567888" });

            migrationBuilder.InsertData(
                table: "SiteState",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "SystemOff", "SystemOffMessage" },
                values: new object[] { new Guid("f812bbf0-20e8-401f-b142-0807641bf9a5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The Website is clossing" });

            migrationBuilder.InsertData(
                table: "TrainingCompany",
                columns: new[] { "Id", "CreatedDate", "Description", "ModifiedDate", "Name", "Picture", "Specialization" },
                values: new object[] { new Guid("273ed3de-dcf0-4062-8d9f-c033bbac6dbe"), new DateTime(2020, 10, 30, 0, 12, 34, 629, DateTimeKind.Local).AddTicks(8024), "HUB Tech partners with its clients becoming part of their support team.  We work beside you to ensure you have a strategy that allows you to transform your Information infrastructure to keep up with the needs of your organization and your users. We have developed proprietary tools and strategies that have enabled us to lower cost and increase the quality of service to our client base, especially to state agencies, municipalities and school districts, where cost is a deciding factor in everyday decision making.", null, "IT TrainingCompany", null, "Design And Programmer " });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TrainerId",
                table: "Courses",
                column: "TrainerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "SiteState");

            migrationBuilder.DropTable(
                name: "TrainingCompany");

            migrationBuilder.DropTable(
                name: "Trainers");
        }
    }
}
