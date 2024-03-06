using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Banco
{
    internal class LojaPecaDAO<T>(LojaPecaContext context) where T : class // classe responsável pelo CRUD.
    {
        

        private DbSet<T> dbSet { get; set; } = context.Set<T>();
       
        public IEnumerable<T> Listar()
        {
            return dbSet.ToList(); 
        }

        public void Adicionar(T valor)
        {
             
            dbSet.Add(valor);
            context.SaveChanges(); 
        }
        public void Atualizar(T valor)
        {
            dbSet.Update(valor); 
            context.SaveChanges(); 
        }
        public void Deletar(T valor)
        {
            dbSet.Remove(valor); 
            context.SaveChanges();
        }
        public void DeletarTodos(Func<T, bool> condicao)
        {
            var list = dbSet.Where(condicao);
            dbSet.RemoveRange(list);
            context.SaveChanges(); 
        }

        public T? RecuperarUmPor(Func<T, bool> condicao)
        {
            return dbSet.FirstOrDefault(condicao);
        }

        public IEnumerable<T> RecuperarListaPor(Func<T, bool> condicao)
        {
            return dbSet.Where(condicao);
        }
    }
}

