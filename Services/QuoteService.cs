using System.Net.Http.Json;
using DnevniCitati.Models;

namespace DnevniCitati.Services;

public class QuoteService
{
    // Za početak aplikacija koristi probne citate, pa može da radi i bez API-ja.
    // Kada kolega završi API, postavi UseApi na true i unesi pravu adresu.
    private const bool UseApi = false;
    private const string ApiBaseUrl = "http://10.0.2.2:5000/";
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri(ApiBaseUrl),
        Timeout = TimeSpan.FromSeconds(10)
    };

    private readonly List<Quote> _sampleQuotes =
    [
        new() { Id = 1, Text = "Uspeh je zbir malih napora koji se ponavljaju iz dana u dan.", Author = "Robert Kolijer", Category = "Život" },
        new() { Id = 2, Text = "Veruj da možeš i već si na pola puta.", Author = "Teodor Ruzvelt", Category = "Život" },
        new() { Id = 3, Text = "Talent pobeđuje utakmice, ali timski rad i inteligencija osvajaju prvenstva.", Author = "Majkl Džordan", Category = "Sport" },
        new() { Id = 4, Text = "Nikada ne odustaj od onoga o čemu ne možeš da prođeš dan a da ne razmišljaš.", Author = "Vinston Čerčil", Category = "Život" },
        new() { Id = 5, Text = "Ljubav nije u tome da gledamo jedno drugo, već da zajedno gledamo u istom pravcu.", Author = "Antoan de Sent Egziperi", Category = "Ljubav" },
        new() { Id = 6, Text = "Vera je ptica koja oseća svetlost i peva kada je zora još mračna.", Author = "Rabindranat Tagore", Category = "Vera" },
        new() { Id = 7, Text = "Pobednik je sanjar koji nikada ne odustaje.", Author = "Nelson Mandela", Category = "Sport" },
        new() { Id = 8, Text = "Najbolje i najlepše stvari na svetu ne mogu se videti ni dodirnuti, već se osećaju srcem.", Author = "Helen Keler", Category = "Ljubav" },
        new() { Id = 9, Text = "Nada je sposobnost da vidimo da postoji svetlost uprkos svoj tami.", Author = "Dezmond Tutu", Category = "Vera" },
        new() { Id = 10, Text = "Svaki dan je nova prilika da promeniš svoj život.", Author = "Dnevni citati", Category = "Život" }
    ];

    public Task<List<Quote>> GetQuotesAsync()
    {
        if (!UseApi)
            return Task.FromResult(_sampleQuotes.Select(Clone).ToList());

        return GetFromApiAsync();
    }

    private async Task<List<Quote>> GetFromApiAsync()
    {
        var quotes = await _httpClient.GetFromJsonAsync<List<Quote>>("api/quotes");
        return quotes ?? [];
    }

    public static Quote Clone(Quote quote) => new()
    {
        Id = quote.Id,
        Text = quote.Text,
        Author = quote.Author,
        Category = quote.Category,
        IsFavorite = quote.IsFavorite
    };
}
