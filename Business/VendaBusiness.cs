using LojaPeca.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Business
{
    internal class VendaBusiness
    {
        private Dictionary<int, Venda> setVenda = new Dictionary<int, Venda>();
        HashSet<Peca> pecas = new HashSet<Peca>();
    }
}
Crie hashSet das classes Venda e Peça pelo id, e hashSet da classe venda peça pelos 3 atributos

Lembrete: Em casos de iteração com uma Collection, tentar usar os metodos disponiveis na classe stream. ex: lista.stream().filter(v->v.quantidade > 1)

Atentar - se para a nomenclatura correta dos metodos e atributos

Criar metodos reunitilizaveis se necessário.
