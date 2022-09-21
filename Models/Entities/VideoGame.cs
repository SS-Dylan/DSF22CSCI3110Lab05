namespace DSF22CSCI3110Lab05.Models.Entities;

public class VideoGame
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Developer { get; set; } = string.Empty;
    public Rating Rating { get; set; }
    public int Year { get; set; }
}
