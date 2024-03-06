using LojaPeca.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Banco
{
    internal class LojaPecaContext : DbContext //* CONTEXTO É A REPRESENTAÇÃO DE UMA SESSÃO NO BANCO DE DADOS (REALIZAÇÃO DE UMA SESSÃO DE CONSULTA, PESQUISA ETC). Corresponde às tabelas do banco de dados*//
    {
        public DbSet<Peca> Peca {get; set;} // mapeia peça

        public DbSet<Venda> Venda { get; set; } // mapeia venda

        public DbSet<VendaPeca> VendaPeca { get; set; } // mapeia vendapeça


        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LojaPeca;Integrated Security=True; MultipleActiveResultSets=True;Connect Timeout=30;Encrypt=False;Trust Server " +
            "Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();

        }
    }
}
