using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyTracker.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCompanyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_company_contact_info_company_id",
                table: "CompanyContactInfo");

            migrationBuilder.DropIndex(
                name: "ix_company_applied_check_list_company_id",
                table: "CompanyAppliedCheckList");

            migrationBuilder.AlterColumn<string>(
                name: "contact_person_phone",
                table: "CompanyContactInfo",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "contact_person_email",
                table: "CompanyContactInfo",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "contact_person",
                table: "CompanyContactInfo",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "org_number",
                table: "Company",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Company",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "city",
                table: "Company",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "ix_company_contact_info_company_id",
                table: "CompanyContactInfo",
                column: "company_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_company_applied_check_list_company_id",
                table: "CompanyAppliedCheckList",
                column: "company_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_company_contact_info_company_id",
                table: "CompanyContactInfo");

            migrationBuilder.DropIndex(
                name: "ix_company_applied_check_list_company_id",
                table: "CompanyAppliedCheckList");

            migrationBuilder.AlterColumn<string>(
                name: "contact_person_phone",
                table: "CompanyContactInfo",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "contact_person_email",
                table: "CompanyContactInfo",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "contact_person",
                table: "CompanyContactInfo",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "org_number",
                table: "Company",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Company",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "city",
                table: "Company",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.CreateIndex(
                name: "ix_company_contact_info_company_id",
                table: "CompanyContactInfo",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_company_applied_check_list_company_id",
                table: "CompanyAppliedCheckList",
                column: "company_id");
        }
    }
}

