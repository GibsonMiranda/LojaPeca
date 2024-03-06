using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Banco
{
    internal class LojaPecaDAO<T>(LojaPecaContext context) where T : class
    {
        

        private DbSet<T> dbSet { get; set; } = context.Set<T>();
       
        public IEnumerable<T> Listar()
        {
            return dbSet.ToList(); // já pega a lista dos artistas bem rapidin
        }

        public void Adicionar(T valor)
        {
            //using var context = new ScreenSoundContext(); 
            dbSet.Add(valor); // adicionar o artista 
            context.SaveChanges(); // salva a alteração
        }
        public void Atualizar(T valor)
        {
            dbSet.Update(valor); // adicionar o artista 
            context.SaveChanges(); // salva a alteração
        }
        public void Deletar(T valor)
        {
            dbSet.Remove(valor); // adicionar o artista 
            context.SaveChanges(); // salva a alteração
        }
        public void DeletarTodos(Func<T, bool> condicao)
        {
            var list = dbSet.Where(condicao);
            dbSet.RemoveRange(list); // adicionar o artista 
            context.SaveChanges(); // salva a alteração
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

