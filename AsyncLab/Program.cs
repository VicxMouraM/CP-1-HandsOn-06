using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;

// =================== Configuração ===================
const int PBKDF2_ITERATIONS = 50_000;
const int HASH_BYTES = 32;
const string CSV_URL = "https://www.gov.br/receitafederal/dados/municipios.csv";
const string OUT_DIR_NAME = "mun_hash_por_uf_async";

string FormatTempo(long ms) => TimeSpan.FromMilliseconds(ms).ToString(@"m\m\ ss\s\ fff\m\s");

var sw = Stopwatch.StartNew();

string baseDir = Directory.GetCurrentDirectory();
string tempCsvPath = Path.Combine(baseDir, "municipios.csv");
string outRoot = Path.Combine(baseDir, OUT_DIR_NAME);

Console.WriteLine("Baixando CSV de municípios (Receita Federal) ...");
using var httpClient = new HttpClient();
var csvContent = await httpClient.GetStringAsync(CSV_URL);
await File.WriteAllTextAsync(tempCsvPath, csvContent, Encoding.UTF8);

Console.WriteLine("Lendo e parseando o CSV ...");
var linhas = await File.ReadAllLinesAsync(tempCsvPath, Encoding.UTF8);
if (linhas.Length == 0)
{
    Console.WriteLine("Arquivo CSV vazio.");
    return;
}

int startIndex = linhas[0].IndexOf("IBGE", StringComparison.OrdinalIgnoreCase) >= 0 ||
                 linhas[0].IndexOf("UF", StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0;

var municipios = new List<Municipio>(linhas.Length - startIndex);

Parallel.For(startIndex, linhas.Length, i =>
{
    var linha = (linhas[i] ?? "").Trim();
    if (string.IsNullOrWhiteSpace(linha)) return;

    var parts = linha.Split(';');
    if (parts.Length < 5) return;

    lock (municipios)
    {
        municipios.Add(new Municipio
        {
            Tom = Util.San(parts[0]),
            Ibge = Util.San(parts[1]),
            NomeTom = Util.San(parts[2]),
            NomeIbge = Util.San(parts[3]),
            Uf = Util.San(parts[4]).ToUpperInvariant()
        });
    }
});

Console.WriteLine($"Registros lidos: {municipios.Count}");

var porUf = new Dictionary<string, List<Municipio>>(StringComparer.OrdinalIgnoreCase);
foreach (var m in municipios)
{
    if (!porUf.ContainsKey(m.Uf))
        porUf[m.Uf] = new List<Municipio>();
    porUf[m.Uf].Add(m);
}

var ufsOrdenadas = porUf.Keys
    .Where(uf => !string.Equals(uf, "EX", StringComparison.OrdinalIgnoreCase))
    .OrderBy(uf => uf, StringComparer.OrdinalIgnoreCase)
    .ToList();

Directory.CreateDirectory(outRoot);
Console.WriteLine("Calculando hash por município e gerando arquivos por UF (paralelamente) ...");

var tasksUfs = ufsOrdenadas.Select(async uf =>
{
    var listaUf = porUf[uf];
    listaUf.Sort((a, b) => string.Compare(a.NomePreferido, b.NomePreferido, StringComparison.OrdinalIgnoreCase));

    Console.WriteLine($"Processando UF: {uf} ({listaUf.Count} municípios)");
    var swUf = Stopwatch.StartNew();

    var linhasCsv = new List<string>();
    var objetosJson = new List<object>();

    // Processamento paralelo dos municípios dentro da UF
    var results = new (Municipio m, string hash)[listaUf.Count];
    Parallel.For(0, listaUf.Count, idx =>
    {
        var m = listaUf[idx];
        string password = m.ToConcatenatedString();
        byte[] salt = Util.BuildSalt(m.Ibge);
        string hashHex = Util.DeriveHashHex(password, salt, PBKDF2_ITERATIONS, HASH_BYTES);
        results[idx] = (m, hashHex);
    });

    foreach (var (m, hashHex) in results)
    {
        linhasCsv.Add($"{m.Tom};{m.Ibge};{m.NomeTom};{m.NomeIbge};{m.Uf};{hashHex}");
        objetosJson.Add(new
        {
            m.Tom,
            m.Ibge,
            m.NomeTom,
            m.NomeIbge,
            m.Uf,
            Hash = hashHex
        });
    }

    string outPathCsv = Path.Combine(outRoot, $"municipios_hash_{uf}.csv");
    await File.WriteAllLinesAsync(outPathCsv, new[] { "TOM;IBGE;NomeTOM;NomeIBGE;UF;Hash" }.Concat(linhasCsv), Encoding.UTF8);

    string jsonPath = Path.Combine(outRoot, $"municipios_hash_{uf}.json");
    var json = JsonSerializer.Serialize(objetosJson, new JsonSerializerOptions { WriteIndented = true });
    await File.WriteAllTextAsync(jsonPath, json, Encoding.UTF8);

    swUf.Stop();
    Console.WriteLine($"UF {uf} concluída. Tempo: {FormatTempo(swUf.ElapsedMilliseconds)}");
});

await Task.WhenAll(tasksUfs);

sw.Stop();
Console.WriteLine();
Console.WriteLine("===== RESUMO =====");
Console.WriteLine($"UFs geradas: {ufsOrdenadas.Count}");
Console.WriteLine($"Pasta de saída: {outRoot}");
Console.WriteLine($"Tempo total: {FormatTempo(sw.ElapsedMilliseconds)} ({sw.Elapsed})");