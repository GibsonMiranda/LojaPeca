using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaPeca.Migrations
{
    /// <inheritdoc />
    public partial class Alteracoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_vendas",
                table: "vendas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pecas",
                table: "pecas");

            migrationBuilder.RenameTable(
                name: "vendas",
                newName: "Venda");

            migrationBuilder.RenameTable(
                name: "pecas",
                newName: "Peca");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Venda",
                table: "Venda",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Peca",
                table: "Peca",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "VendaPeca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendaId = table.Column<int>(type: "int", nullable: true),
                    PecaId = table.Column<int>(type: "int", nullable: true),
                    ValorUnitario = table.Column<double>(type: "float", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaPeca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendaPeca_Peca_PecaId",
                        column: x => x.PecaId,
                        principalTable: "Peca",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VendaPeca_Venda_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Venda",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendaPeca_PecaId",
                table: "VendaPeca",
                column: "PecaId");

            migrationBuilder.CreateIndex(
                name: "IX_VendaPeca_VendaId",
                table: "VendaPeca",
                column: "VendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendaPeca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Venda",
                table: "Venda");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Peca",
                table: "Peca");

            migrationBuilder.RenameTable(
                name: "Venda",
                newName: "vendas");

            migrationBuilder.RenameTable(
                name: "Peca",
                newName: "pecas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vendas",
                table: "vendas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pecas",
                table: "pecas",
                column: "Id");
        }
    }
}
