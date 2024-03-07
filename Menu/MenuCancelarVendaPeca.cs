using LojaPeca.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Menu
{
    internal class MenuCancelarVendaPeca : Menu
    {
        public override void Executar()
        {
            base.Executar();
            ExibirTituloDaOpcao("Cancelar Venda Peça");
            Console.WriteLine("Digite o id da venda");
            var venda = Console.ReadLine()!;
            var idVenda = Convert.ToInt32(venda);
            Console.WriteLine("Digite o id da peça");
            var peca = Console.ReadLine()!;
            var idPeca = Convert.ToInt32(peca);
            Console.WriteLine("Digite a quantidade");
            var quantidade = Console.ReadLine()!;
            var qtd = Convert.ToInt32(quantidade);
            VendaBusiness vendaBusiness = new VendaBusiness();
            vendaBusiness.CancelarVendaPeca(idVenda, idPeca, qtd);
            Console.WriteLine("Venda Peça cancelada com sucesso! Pressione qualquer tecla para sair.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
