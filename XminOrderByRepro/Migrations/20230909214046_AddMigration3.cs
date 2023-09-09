using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XminOrderByRepro.Migrations
{
    /// <inheritdoc />
    public partial class AddMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Json",
                table: "Foo",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Json",
                table: "Foo");
        }
    }
}
