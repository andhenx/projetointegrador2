using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using ProjetoIntegrador2.API.Entities;
using System.Globalization;

namespace ProjetoIntegrador2.API.Data.Seeds;

internal sealed class MovimentoCsvRecord
{
    [Name("TIPO")]
    public string Tipo { get; set; } = string.Empty;

    [Name("DATA")]
    public DateTime Data { get; set; }

    [Name("NUMNT")]
    public string NumNt { get; set; } = string.Empty;

    [Name("SERIE")]
    public string Serie { get; set; } = string.Empty;

    [Name("CHAVE")]
    public string Chave { get; set; } = string.Empty;

    [Name("CODIGO")]
    public string Codigo { get; set; } = string.Empty;

    [Name("DESCRICAO")]
    public string Descricao { get; set; } = string.Empty;

    [Name("QTDE")]
    public decimal Qtde { get; set; }

    [Name("PRECO_UNIT")]
    public decimal PrecoUnit { get; set; }

    [Name("TOTAL")]
    public decimal Total { get; set; }
}

public static class MovimentoSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Movimentos.Any())
            return;

        string path = Path.Combine(AppContext.BaseDirectory, "Data", "Seeds", "movimentos.csv");
        if (!File.Exists(path))
            path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Seeds", "movimentos.csv");

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ",",
            BadDataFound = null
        };

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);

        var records = csv.GetRecords<MovimentoCsvRecord>()
            .Select(r => new Movimento
            {
                Id = Guid.NewGuid(),
                Tipo = r.Tipo,
                Data = r.Data,
                NumNt = r.NumNt,
                Serie = r.Serie,
                Chave = r.Chave,
                Codigo = r.Codigo,
                Descricao = r.Descricao,
                Qtde = r.Qtde,
                PrecoUnit = r.PrecoUnit,
                Total = r.Total
            })
            .ToList();

        db.Movimentos.AddRange(records);
        db.SaveChanges();
    }
}
