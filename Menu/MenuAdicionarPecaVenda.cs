using LojaPeca.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Menu
{
    internal class MenuAdicionarPecaVenda : Menu
    {
        public override void Executar()
        {
            base.Executar();
            ExibirTituloDaOpcao("Adicionar Peça à Venda");
            Console.WriteLine("Digite o id da venda");
            var venda = Console.ReadLine()!;
            var idVenda = Convert.ToInt32(venda);

            Console.WriteLine("Digite o id da peça");
            var valor = Console.ReadLine()!;
            var idPeca = Convert.ToInt32(valor);

            Console.WriteLine("Digite a quantidade");
            var qtd = Console.ReadLine()!;
            var quantidade = Convert.ToInt32(qtd);

            VendaBusiness vendaBusiness = new VendaBusiness();
            vendaBusiness.AdicionarPecaVenda(idVenda, idPeca, quantidade);
            Console.Clear();
        }
    }
}
