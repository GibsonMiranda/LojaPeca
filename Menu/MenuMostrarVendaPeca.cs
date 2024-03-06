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
            LojaPecaDAO<VendaPeca> vendaPeca = new LojaPecaDAO<VendaPeca>(new LojaPecaContext());
            foreach (var vendaP in vendaPeca.Listar())
            {
                Console.WriteLine(vendaP.ToString());
            }

            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
