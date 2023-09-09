using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XminOrderByRepro.Migrations
{
    /// <inheritdoc />
    public partial class AddMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Json",
                table: "Foo",
                newName: "JsonBlob");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JsonBlob",
                table: "Foo",
                newName: "Json");
        }
    }
}
