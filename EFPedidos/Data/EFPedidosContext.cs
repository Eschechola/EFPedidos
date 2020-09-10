using EFPedidos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace EFPedidos.Data
{
    public class EFPedidosContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> PedidosItens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options
                //gera log do EF no console
                .UseLoggerFactory(_logger)
                //habilita exibir os dados no console
                .EnableSensitiveDataLogging()
                .UseSqlServer(@"Data Source=DESKTOP-652APCE\SQLEXPRESS;Initial Catalog=EFPEDIDOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                //tenta se conectar 6x em caso de falhas
                //ou passa a quantidade de vezes e o delay
                x => x.EnableRetryOnFailure(maxRetryCount: 20, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //mapeia todas as classes do assembly que implementan IEntityTypeConfiguration (projeto indicado)
            builder.ApplyConfigurationsFromAssembly(typeof(EFPedidosContext).Assembly);

            //builder.ApplyConfiguration(new ClienteConfiguration());

            //builder.ApplyConfiguration(new ProdutoConfiguration());

            //builder.ApplyConfiguration(new PedidoConfiguration());

            //builder.ApplyConfiguration(new ItemPedidoConfiguration());
        }
    }
}
