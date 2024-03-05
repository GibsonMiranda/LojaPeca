using LojaPeca.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Banco
{
    internal class LojaPecaContext : DbContext
    {
        public DbSet<Peca> Peca {get; set;}

        public DbSet<Venda> Venda { get; set; }

        public DbSet<VendaPeca> VendaPeca { get; set; }


        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LojaPeca;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server " +
            "Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();

        }
    }
}
