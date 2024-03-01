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

