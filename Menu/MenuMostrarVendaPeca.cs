using LojaPeca.Banco;
using LojaPeca.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Menu
{
    internal class MenuMostrarVendaPeca : Menu
    {
        public override void Executar()
        {
            base.Executar();
            ExibirTituloDaOpcao("Mostrar Relação de Vendas");
            Thread.Sleep(3000);
            LojaPecaDAO<VendaPeca> vendaPeca = new LojaPecaDAO<VendaPeca>();
            foreach (var vendaP in vendaPeca.Listar())
            {
                Console.WriteLine($"ID VendaPeça: {vendaP.Id} \n " +
                    $"- ID Venda: {vendaP.Venda.Id} - " +
                    $"ID Peça: {vendaP.Peca.Id} - Qtd {vendaP.Quantidade} - Valor Unitário: R$ {vendaP.Peca.Valor} - Total: R$ {vendaP.Quantidade * vendaP.Peca.Valor}");
            }

            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
