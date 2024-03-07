using LojaPeca.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Menu
{
    internal class MenuCancelarTodaVendaPeca : Menu
    {
        public override void Executar()
        {
            base.Executar();
            ExibirTituloDaOpcao("Cancelar Toda Venda Peça");
            Console.WriteLine("Digite o id da venda");
            var venda = Console.ReadLine()!;
            var idVenda = Convert.ToInt32(venda);
            Console.WriteLine("Digite o id da peça");
            var peca = Console.ReadLine()!;
            var idPeca = Convert.ToInt32(peca);
            VendaBusiness vendaBusiness = new VendaBusiness();
            vendaBusiness.CancelarTodaPecaVenda(idVenda, idPeca);
            Console.WriteLine("Toda Venda Peça foi com sucesso! Pressione qualquer tecla para sair.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
