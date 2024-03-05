using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaPeca.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoNulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendaPeca_Peca_PecaId",
                table: "VendaPeca");

            migrationBuilder.DropForeignKey(
                name: "FK_VendaPeca_Venda_VendaId",
                table: "VendaPeca");

            migrationBuilder.AlterColumn<int>(
                name: "VendaId",
                table: "VendaPeca",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PecaId",
                table: "VendaPeca",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Peca",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VendaPeca_Peca_PecaId",
                table: "VendaPeca",
                column: "PecaId",
                principalTable: "Peca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VendaPeca_Venda_VendaId",
                table: "VendaPeca",
                column: "VendaId",
                principalTable: "Venda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendaPeca_Peca_PecaId",
                table: "VendaPeca");

            migrationBuilder.DropForeignKey(
                name: "FK_VendaPeca_Venda_VendaId",
                table: "VendaPeca");

            migrationBuilder.AlterColumn<int>(
                name: "VendaId",
                table: "VendaPeca",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PecaId",
                table: "VendaPeca",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Peca",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_VendaPeca_Peca_PecaId",
                table: "VendaPeca",
                column: "PecaId",
                principalTable: "Peca",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendaPeca_Venda_VendaId",
                table: "VendaPeca",
                column: "VendaId",
                principalTable: "Venda",
                principalColumn: "Id");
        }
    }
}
