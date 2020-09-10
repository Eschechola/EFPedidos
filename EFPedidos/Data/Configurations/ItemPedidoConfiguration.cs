using EFPedidos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFPedidos.Data.Configurations
{
    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.ToTable("PedidoItens");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Quantidade)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.Valor)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.Desconto)
                .HasDefaultValue(0)
                .IsRequired();
        }
    }
}
