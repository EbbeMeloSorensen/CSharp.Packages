using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkNet6.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedBulldog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Dogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Dogs",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Dogs");
        }
    }
}
