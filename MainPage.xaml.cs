using System.Collections.ObjectModel;
using DnevniCitati.Models;
using DnevniCitati.Services;

namespace DnevniCitati;

public partial class MainPage : ContentPage
{
    private readonly QuoteService _quoteService = new();
    private readonly ObservableCollection<QuoteViewModel> _allQuotes = [];
    private bool _showFavoritesOnly;

    public MainPage()
    {
        InitializeComponent();
        CategoryPicker.SelectedIndex = 0;
        Loaded += async (_, _) => await LoadQuotesAsync();
    }

    private async Task LoadQuotesAsync()
    {
        try
        {
            StatusLabel.Text = "Učitavanje citata...";
            var quotes = await _quoteService.GetQuotesAsync();

            _allQuotes.Clear();
            foreach (var quote in quotes)
                _allQuotes.Add(new QuoteViewModel(quote));

            ApplyFilters();
        }
        catch (Exception)
        {
            StatusLabel.Text = "Nije moguće učitati citate. Proveri API adresu i internet vezu.";
        }
    }

    private void ApplyFilters()
    {
        var search = SearchInput.Text?.Trim() ?? string.Empty;
        var category = CategoryPicker.SelectedItem?.ToString() ?? "Sve kategorije";

        var filtered = _allQuotes.Where(q =>
            (category == "Sve kategorije" || q.Category.Equals(category, StringComparison.OrdinalIgnoreCase)) &&
            (!_showFavoritesOnly || q.IsFavorite) &&
            (string.IsNullOrWhiteSpace(search) ||
             q.Text.Contains(search, StringComparison.OrdinalIgnoreCase) ||
             q.Author.Contains(search, StringComparison.OrdinalIgnoreCase)));

        var result = filtered.ToList();
        QuotesList.ItemsSource = result;
        StatusLabel.Text = result.Count == 0
            ? "Nema citata koji odgovaraju pretrazi."
            : $"Prikazano citata: {result.Count}";
        FavoritesButton.Text = _showFavoritesOnly ? "♥ Omiljeni" : "♡ Omiljeni";
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e) => ApplyFilters();

    private void OnCategoryChanged(object sender, EventArgs e) => ApplyFilters();

    private void OnFavoritesClicked(object sender, EventArgs e)
    {
        _showFavoritesOnly = !_showFavoritesOnly;
        ApplyFilters();
    }

    private void OnFavoriteClicked(object sender, EventArgs e)
    {
        if (sender is Button { CommandParameter: QuoteViewModel quote })
        {
            quote.IsFavorite = !quote.IsFavorite;
            ApplyFilters();
        }
    }

    private sealed class QuoteViewModel : BindableObject
    {
        private bool _isFavorite;

        public int Id { get; }
        public string Text { get; }
        public string Author { get; }
        public string Category { get; }

        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                if (_isFavorite == value) return;
                _isFavorite = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FavoriteButtonText));
            }
        }

        public string FavoriteButtonText => IsFavorite ? "♥" : "♡";

        public QuoteViewModel(Quote quote)
        {
            Id = quote.Id;
            Text = quote.Text;
            Author = quote.Author;
            Category = quote.Category;
            IsFavorite = quote.IsFavorite;
        }
    }
}
