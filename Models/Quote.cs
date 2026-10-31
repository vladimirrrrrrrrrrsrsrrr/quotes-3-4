namespace DnevniCitati.Models;

public class Quote
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Author { get; set; } = "Nepoznat autor";
    public string Category { get; set; } = "Život";
    public bool IsFavorite { get; set; }
}
