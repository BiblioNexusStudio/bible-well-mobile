using System.Diagnostics;
using BibleWell.App.Data.Models;
using SQLite;

namespace BibleWell.App.Data;

public class BibleWellSqlite
{
    private SQLiteAsyncConnection? _database;

    public BibleWellSqlite()
    {
    }

    private async Task InitAsync()
    {
        if (_database is not null)
        {
            return;
        }
        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await _database.CreateTableAsync<ExampleItem>();
        Debug.WriteLine("ExampleItem table Created");
    }

    public async Task<List<ExampleItem>> GetItemsAsync()
    {
        await InitAsync();
        return await _database!.Table<ExampleItem>().ToListAsync();
    }
    
    public async Task<int> SaveItemAsync(ExampleItem item)
    {
        await InitAsync();
        if (item.Id != 0)
        {
            return await _database!.UpdateAsync(item);
        }
        else
        {
            return await _database!.InsertAsync(item);
        }
    }

    public async Task<int> DeleteItemAsync(ExampleItem item)
    {
        await InitAsync();
        return await _database!.DeleteAsync(item);
    }
}

