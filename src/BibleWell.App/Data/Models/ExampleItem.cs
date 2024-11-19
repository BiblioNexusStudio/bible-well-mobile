using SQLite;

namespace BibleWell.App.Data.Models;

public class ExampleItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; init; }
    public string? Name { get; set; }
}

