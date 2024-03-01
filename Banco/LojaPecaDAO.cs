using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Banco
{
    internal abstract class DAO<T> where T : class
    {
        private readonly LojaPecaContext context = new LojaPecaContext();
        public DAO(LojaPecaContext context)
        {
            this.context = context;
        }
    }
}
