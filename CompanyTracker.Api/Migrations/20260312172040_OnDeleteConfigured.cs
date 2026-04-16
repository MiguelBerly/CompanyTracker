using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CompanyTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class OnDeleteConfigured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_company_applied_check_list_company_company_id",
                table: "CompanyAppliedCheckList");

            migrationBuilder.DropForeignKey(
                name: "fk_company_contact_info_company_company_id",
                table: "CompanyContactInfo");

            migrationBuilder.DropIndex(
                name: "ix_company_contact_info_company_id",
                table: "CompanyContactInfo");

            migrationBuilder.DropIndex(
                name: "ix_company_applied_check_list_company_id",
                table: "CompanyAppliedCheckList");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "CompanyContactInfo",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "CompanyAppliedCheckList",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "fk_company_applied_check_list_company_id",
                table: "CompanyAppliedCheckList",
                column: "id",
                principalTable: "Company",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_company_contact_info_company_id",
                table: "CompanyContactInfo",
                column: "id",
                principalTable: "Company",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_company_applied_check_list_company_id",
                table: "CompanyAppliedCheckList");

            migrationBuilder.DropForeignKey(
                name: "fk_company_contact_info_company_id",
                table: "CompanyContactInfo");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "CompanyContactInfo",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "CompanyAppliedCheckList",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

            migrationBuilder.AddForeignKey(
                name: "fk_company_applied_check_list_company_company_id",
                table: "CompanyAppliedCheckList",
                column: "company_id",
                principalTable: "Company",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_company_contact_info_company_company_id",
                table: "CompanyContactInfo",
                column: "company_id",
                principalTable: "Company",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

