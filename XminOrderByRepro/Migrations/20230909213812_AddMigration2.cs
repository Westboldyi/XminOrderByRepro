using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XminOrderByRepro.Migrations
{
    /// <inheritdoc />
    public partial class AddMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bar_Foo_FooId",
                table: "Bar");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Foo");

            migrationBuilder.AlterColumn<string>(
                name: "FooId",
                table: "Bar",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Bar_Foo_FooId",
                table: "Bar",
                column: "FooId",
                principalTable: "Foo",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bar_Foo_FooId",
                table: "Bar");

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Foo",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AlterColumn<string>(
                name: "FooId",
                table: "Bar",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bar_Foo_FooId",
                table: "Bar",
                column: "FooId",
                principalTable: "Foo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
