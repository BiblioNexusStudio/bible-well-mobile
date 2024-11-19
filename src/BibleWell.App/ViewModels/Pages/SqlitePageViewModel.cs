using System.Collections.ObjectModel;
using BibleWell.App.Data;
using BibleWell.App.Data.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BibleWell.App.ViewModels.Pages;
public sealed partial class SqlitePageViewModel : PageViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia with SQLite!";
    
    [ObservableProperty]
    private string _tableText = "Click the button to fetch from the Db.";
    
    public ObservableCollection<ExampleItem> ExampleItems { get; set; }

    private readonly BibleWellSqlite _dbContext;
    
    // todo: hide grid until populated?
    // todo: show simple text 'loading ...'?
    // todo: update items?
    public SqlitePageViewModel()
    {
        _dbContext = new BibleWellSqlite();
        ExampleItems = new ObservableCollection<ExampleItem>([]);
    }
    
    [RelayCommand]
    private async Task GetItemsButtonClick()
    {
        TableText = "Items fetched. Click again to refresh the list from the DB.";
        ExampleItems.Clear();
        
        var dbItems = await _dbContext.GetItemsAsync();

        foreach (var item in dbItems)
        {
            ExampleItems.Add(item);
        }
    }

    [ObservableProperty]
    private string _newItemText = "";
    [RelayCommand]
    private async Task CreateItem()
    {
        await _dbContext.SaveItemAsync(new ExampleItem()
        {
            Name = NewItemText,
        });
        
        NewItemText = string.Empty;
        
        await GetItemsButtonClick();
    }
    
    // todo: generic save item fn that can be used for create or update?

    [RelayCommand]
    private async Task DeleteItem(int itemId)
    {
        var item = ExampleItems.FirstOrDefault(i => i.Id == itemId);

        if (item is null)
        {
            return;
        }
        
        await _dbContext.DeleteItemAsync(item);
        await GetItemsButtonClick();
    }
}

