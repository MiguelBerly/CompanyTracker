using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyTracker.Migrations
{
    /// <inheritdoc />
    public partial class RemovedNavPropFromCompanyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_company_applied_check_list_company_id",
                table: "CompanyAppliedCheckList");

            migrationBuilder.DropColumn(
                name: "applied_checklist_id",
                table: "Company");

            migrationBuilder.CreateIndex(
                name: "ix_company_applied_check_list_company_id",
                table: "CompanyAppliedCheckList",
                column: "company_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_company_applied_check_list_company_id",
                table: "CompanyAppliedCheckList");

            migrationBuilder.AddColumn<int>(
                name: "applied_checklist_id",
                table: "Company",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_company_applied_check_list_company_id",
                table: "CompanyAppliedCheckList",
                column: "company_id",
                unique: true);
        }
    }
}

