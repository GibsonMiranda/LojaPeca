using LojaPeca.Business;
using LojaPeca.Menu;
using Microsoft.EntityFrameworkCore.Query.Internal;



IDictionary<int, Menu> opcoes = new Dictionary<int, Menu>();
opcoes.Add(1, new MenuCriarPeca());
opcoes.Add(2, new MenuAdicionarPecaVenda());
opcoes.Add(3, new MenuMostrarVendaPeca());
opcoes.Add(4, new MenuLimparVenda());


void ExibirLogo()
{
    Console.WriteLine("***********************");
    Console.WriteLine("LOJA DE PEÇAS GUANABARA");
    Console.WriteLine("***********************");
}

void ExibirOpcoesDoMenu()
{
    ExibirLogo();
    Console.WriteLine("\nDigite 1 para criar uma peça");
    Console.WriteLine("Digite 2 para adicionar uma peça à venda existente");
    Console.WriteLine("Digite 3 para mostrar as relações de vendas");
    Console.WriteLine("Digite 4 para limpar uma venda");
    //Console.WriteLine("Digite -1 para sair");

    Console.Write("\nDigite a sua opção: ");
    string opcaoEscolhida = Console.ReadLine()!;
    int opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);

    if (opcoes.ContainsKey(opcaoEscolhidaNumerica))
    {
        Menu menuASerExibido = opcoes[opcaoEscolhidaNumerica];
        menuASerExibido.Executar();
        if (opcaoEscolhidaNumerica > 0) ExibirOpcoesDoMenu();
    }
    else
    {
        Console.WriteLine("Opção inválida");
    }
}
try
{
    ExibirOpcoesDoMenu();
} catch(Exception ex)
{
    Console.WriteLine(ex.Message);
  
    Console.WriteLine("digite qualquer tecla para continuar");
    Console.ReadLine();
    Console.Clear();
    ExibirOpcoesDoMenu();
}

