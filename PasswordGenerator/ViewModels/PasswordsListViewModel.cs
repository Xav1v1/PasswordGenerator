using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using PasswordGenerator.Db;
using PasswordGenerator.Models;
using PasswordGenerator.Views;
using ReactiveUI;

namespace PasswordGenerator.ViewModels;

public class PasswordsListViewModel : ViewModelBase
{
    private object? _selectedItem;
    
    public ObservableCollection<PasswordModel?> Passwords { get; }
    public ICommand DeleteCommand { get; }
    public ICommand CopyCommand { get; }
    
    public object SelectedItem
    {
        get => _selectedItem;
        set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
    }
    
    public PasswordsListViewModel()
    {
        Passwords = new ObservableCollection<PasswordModel?>();

        DeleteCommand = ReactiveCommand.Create(() =>
        {
            using var context = new PasswordContext();
            var model = (PasswordModel)_selectedItem!;
            Passwords.Remove(model);
            context.PasswordModels.Remove(model);
            context.SaveChanges();
        });

        CopyCommand = ReactiveCommand.Create(Copy);
        
        LoadPasswords();
    }

    private void Copy()
    {
        if (_selectedItem == null) return;
        var clipboard = MainWindow.Clipboard;
        clipboard?.SetTextAsync(((PasswordModel) _selectedItem).Password);
    }

    private void LoadPasswords()
    {
        Passwords.Clear();
        
        using var context = new PasswordContext();
        foreach (var password in context.PasswordModels)
        {
            Passwords.Add(password);
        }
    }
}