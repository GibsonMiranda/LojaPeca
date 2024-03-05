using LojaPeca.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Menu
{
    internal class MenuCriarPeca : Menu
    {
        public override void Executar()
        {
            base.Executar();
            ExibirTituloDaOpcao("Criar Peça");
            Console.WriteLine("Digite a descrição da peça");
            var descricao = Console.ReadLine()!;
            Console.WriteLine("Digite o valor da peça");
            var valor = Console.ReadLine()!;
            var valorPeca = Convert.ToInt32(valor);
            VendaBusiness vendaBusiness = new VendaBusiness();
            vendaBusiness.CriarPeca(descricao, valorPeca);
            Console.WriteLine("Peça Criada Com Sucesso!");
            Console.Clear();
        }
     
    }
}
