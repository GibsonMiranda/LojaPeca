using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Banco
{
    internal class LojaPecaDAO<T> where T : class
    {
        protected readonly LojaPecaContext context = new LojaPecaContext(); // protected permite que a classe herdeira utilize metodos e membros

        
        public IEnumerable<T> Listar()
        {
            return context.Set<T>().ToList(); // já pega a lista dos artistas bem rapidin
        }

        public void Adicionar(T valor)
        {
            //using var context = new ScreenSoundContext(); 
            context.Set<T>().Add(valor); // adicionar o artista 
            context.SaveChanges(); // salva a alteração
        }
        public void Atualizar(T valor)
        {
            context.Set<T>().Update(valor); // adicionar o artista 
            context.SaveChanges(); // salva a alteração
        }
        public void Deletar(T valor)
        {
            context.Set<T>().Remove(valor); // adicionar o artista 
            context.SaveChanges(); // salva a alteração
        }

        public T? RecuperarUmPor(Func<T, bool> condicao)
        {
            return context.Set<T>().FirstOrDefault(condicao);
        }

        public IEnumerable<T> RecuperarListaPor(Func<T, bool> condicao)
        {
            return context.Set<T>().Where(condicao);
        }
    }
}

