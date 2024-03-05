using LojaPeca.Business;
using LojaPeca.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Menu
{
    internal class MenuLimparVenda : Menu
    {
        public override void Executar()
        {
            base.Executar();
            ExibirTituloDaOpcao("Limpar Venda");
            Console.WriteLine("Digite o id da venda");
            var venda = Console.ReadLine()!;
            var idVenda = Convert.ToInt32(venda);
            VendaBusiness vendaBusiness = new VendaBusiness();
            vendaBusiness.LimparVenda(idVenda);
            Console.WriteLine("alteração realizada com êxito! Pressione qualquer tecla para sair");
            Console.ReadKey();
            Console.Clear();
        }

    }
}

