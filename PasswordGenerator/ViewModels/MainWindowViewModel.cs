using System;
using System.Collections.Generic;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Notification;
using PasswordGenerator.Db;
using PasswordGenerator.Views;
using ReactiveUI;

namespace PasswordGenerator.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private int _passwordLength;
    private string _generatedPassword;
    
    public bool AllowNumbers { get; set; }
    public bool AllowUppercase { get; set; }
    public bool AllowLowercase { get; set; }
    public bool AllowSymbols { get; set; }
    
    public ICommand GenerateCommand { get; }
    public ICommand CopyCommand { get; }
    public ICommand OpenSavePasswordFormCommand { get; }
    public ICommand OpenPasswordsListCommand { get; }

    public int PasswordLength
    {
        get => _passwordLength;
        set => this.RaiseAndSetIfChanged(ref _passwordLength, value);
    }
    public string GeneratedPassword
    {
        get => _generatedPassword;
        set => this.RaiseAndSetIfChanged(ref _generatedPassword, value);
    }

    public MainWindowViewModel()
    {
        _generatedPassword = string.Empty;
        PasswordLength = 8;
        AllowNumbers = true;
        AllowUppercase = true;
        AllowLowercase = true;
        AllowSymbols = true;

        GenerateCommand = ReactiveCommand.Create(Generate);
        CopyCommand = ReactiveCommand.Create(Copy);
        OpenSavePasswordFormCommand = ReactiveCommand.Create(OpenSavePasswordForm);
        OpenPasswordsListCommand = ReactiveCommand.Create(OpenPasswordsListWindow);
    }

    private void Generate()
    {
        var random = new Random();

        const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string symbolChars = "!@#$%^&*()";

        var chars = new List<char>();

        if (AllowLowercase)
            chars.AddRange(lowercaseChars);

        if (AllowUppercase)
            chars.AddRange(uppercaseChars);

        if (AllowNumbers)
            chars.AddRange(new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });

        if (AllowSymbols)
            chars.AddRange(symbolChars);
        
        char[] password = new char[PasswordLength];
        
        for (int i = 0; i < PasswordLength; i++)
        {
            password[i] = chars[random.Next(chars.Count)];
        }

        GeneratedPassword = new string(password);
    }

    private void Copy()
    {
        var clipboard = MainWindow.Clipboard;
        clipboard?.SetTextAsync(GeneratedPassword);
    }

    private void OpenSavePasswordForm()
    {
        var spf = new SavePasswordForm
        {
            DataContext = new SavePasswordViewModel(GeneratedPassword)
        };
        spf.Show();
    }
    
    
    private void OpenPasswordsListWindow()
    {
        var plw = new PasswordsListWindow()
        {
            DataContext = new PasswordsListViewModel()
        };
        
        plw.Show();
    }
}