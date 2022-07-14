using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CustomerRegistration.Data.Migrations
{
    public partial class reportsfive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommercialActivities_Customers_CustomerId",
                table: "CommercialActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCountByCityReportDetails_Reports_ReportId",
                table: "CustomerCountByCityReportDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Reports",
                newName: "CustomerCountByCityReports");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerCountByCityReports",
                table: "CustomerCountByCityReports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TopFiveCustomersByActivityReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopFiveCustomersByActivityReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopFiveCustomersByActivityReportDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    ActivityCount = table.Column<int>(type: "integer", nullable: false),
                    ReportId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopFiveCustomersByActivityReportDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopFiveCustomersByActivityReportDetail_TopFiveCustomersByAc~",
                        column: x => x.ReportId,
                        principalTable: "TopFiveCustomersByActivityReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopFiveCustomersByActivityReportDetail_ReportId",
                table: "TopFiveCustomersByActivityReportDetail",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommercialActivities_Customer_CustomerId",
                table: "CommercialActivities",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCountByCityReportDetails_CustomerCountByCityReports~",
                table: "CustomerCountByCityReportDetails",
                column: "ReportId",
                principalTable: "CustomerCountByCityReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommercialActivities_Customer_CustomerId",
                table: "CommercialActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCountByCityReportDetails_CustomerCountByCityReports~",
                table: "CustomerCountByCityReportDetails");

            migrationBuilder.DropTable(
                name: "TopFiveCustomersByActivityReportDetail");

            migrationBuilder.DropTable(
                name: "TopFiveCustomersByActivityReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerCountByCityReports",
                table: "CustomerCountByCityReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "CustomerCountByCityReports",
                newName: "Reports");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports",
                table: "Reports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommercialActivities_Customers_CustomerId",
                table: "CommercialActivities",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCountByCityReportDetails_Reports_ReportId",
                table: "CustomerCountByCityReportDetails",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
