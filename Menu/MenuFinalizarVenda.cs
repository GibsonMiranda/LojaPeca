using LojaPeca.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Menu
{
    internal class MenuFinalizarVenda : Menu
    {
        public override void Executar()
        {
            base.Executar();
            ExibirTituloDaOpcao("Finalizar a Venda");
            Console.WriteLine("Digite o id da venda");
            var venda = Console.ReadLine()!;
            var idVenda = Convert.ToInt32(venda);
            VendaBusiness vendaBusiness = new VendaBusiness();
            vendaBusiness.FinalizarVenda(idVenda);
            Console.WriteLine("Venda Finalizada com sucesso! Pressione qualquer tecla para sair.");   
            Console.ReadKey();
            Console.Clear();
        }
    }
}
