using LojaPeca.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Menu
{
    internal class MenuCancelarVenda : Menu
    {
        public override void Executar()
        {
            base.Executar();
            ExibirTituloDaOpcao("Cancelar Venda");
            Console.WriteLine("Digite o id da venda");
            var venda = Console.ReadLine()!;
            var idVenda = Convert.ToInt32(venda);
            VendaBusiness vendaBusiness = new VendaBusiness();
            vendaBusiness.CancelarVenda(idVenda);
            Console.WriteLine("Venda cancelada com sucesso! Pressione qualquer tecla para sair.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
