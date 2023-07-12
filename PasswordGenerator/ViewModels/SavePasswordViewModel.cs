using System.Windows.Input;
using Avalonia;
using PasswordGenerator.Db;
using PasswordGenerator.Models;
using PasswordGenerator.Views;
using ReactiveUI;

namespace PasswordGenerator.ViewModels;

public class SavePasswordViewModel : ViewModelBase
{
    private string _keys;
    private string _myPassword;

    public string Keys
    {
        get => _keys;
        set => this.RaiseAndSetIfChanged(ref _keys, value);
    }

    public string MyPassword
    {
        get => _myPassword;
        set => this.RaiseAndSetIfChanged(ref _myPassword, value);
    }

    public ICommand SavePasswordCommand { get; }

    public SavePasswordViewModel(string prevMyPassword)
    {
        Keys = "";
        MyPassword = prevMyPassword;
        SavePasswordCommand = ReactiveCommand.Create(SavePassword);
    }

    private void SavePassword()
    {
        using var context = new PasswordContext();
        var passwordModel = new PasswordModel()
        {
            Key = Keys,
            Password =MyPassword
        };
            
        context.Add(passwordModel);
        context.SaveChanges();
    }
}