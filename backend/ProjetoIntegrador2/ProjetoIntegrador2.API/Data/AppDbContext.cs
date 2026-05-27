using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador2.API.Entities;
using System.Globalization;

namespace ProjetoIntegrador2.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Movimento> Movimentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.ToTable("produtos");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                  .HasColumnName("id")
                  .HasDefaultValueSql("gen_random_uuid()");

            entity.Property(p => p.Codigo)
                  .HasColumnName("codigo")
                  .IsRequired();

            entity.Property(p => p.Descricao)
                  .HasColumnName("descricao")
                  .IsRequired();

            entity.Property(p => p.Ativo)
                  .HasColumnName("ativo");

            entity.Property(p => p.Unid)
                  .HasColumnName("unid")
                  .IsRequired();

            entity.Property(p => p.Custo)
                  .HasColumnName("custo")
                  .HasColumnType("numeric(18,4)");

            entity.Property(p => p.Preco)
                  .HasColumnName("preco")
                  .HasColumnType("numeric(18,4)");

            entity.Property(p => p.Estoque)
                  .HasColumnName("estoque")
                  .HasColumnType("numeric(18,4)");
        });

        modelBuilder.Entity<Movimento>(entity =>
        {
            entity.ToTable("movimentos");

            entity.HasKey(m => m.Id);

            entity.Property(m => m.Id)
                  .HasColumnName("id")
                  .HasDefaultValueSql("gen_random_uuid()");

            entity.Property(m => m.Tipo)
                  .HasColumnName("tipo")
                  .IsRequired();

            entity.Property(m => m.Data)
                  .HasColumnName("data")
                  .IsRequired();

            entity.Property(m => m.NumNt)
                  .HasColumnName("num_nt")
                  .IsRequired();

            entity.Property(m => m.Serie)
                  .HasColumnName("serie")
                  .IsRequired();

            entity.Property(m => m.Chave)
                  .HasColumnName("chave")
                  .IsRequired();

            entity.Property(m => m.Codigo)
                  .HasColumnName("codigo")
                  .IsRequired();

            entity.Property(m => m.Descricao)
                  .HasColumnName("descricao")
                  .IsRequired();

            entity.Property(m => m.Qtde)
                  .HasColumnName("qtde")
                  .HasColumnType("numeric(18,4)");

            entity.Property(m => m.PrecoUnit)
                  .HasColumnName("preco_unit")
                  .HasColumnType("numeric(18,4)");

            entity.Property(m => m.Total)
                  .HasColumnName("total")
                  .HasColumnType("numeric(18,4)");
        });

        // seed via CSV; o HasData do EF Core precisa dos dados em OnModelCreating
        var config = new CsvConfiguration(CultureInfo.GetCultureInfo("pt-BR"))
        {
            HasHeaderRecord = true,
            Delimiter = ","
        };

        // BaseDirectory aponta para bin/ em desenvolvimento e para a raiz no container
        string path = Path.Combine(AppContext.BaseDirectory, "Data", "Seeds", "produtos_com_uuid.csv");
        if (!File.Exists(path))
            path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Seeds", "produtos_com_uuid.csv");

        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<Produto>().ToList();

            modelBuilder.Entity<Produto>().HasData(records);
        }
    }
}
