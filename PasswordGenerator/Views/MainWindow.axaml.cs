using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore;
using PasswordGenerator.Db;
using PasswordGenerator.ViewModels;

namespace PasswordGenerator.Views;

public partial class MainWindow : Window
{
    public static IClipboard? Clipboard;
    public MainWindow()
    {
        InitializeComponent();
        Clipboard = GetTopLevel(this)?.Clipboard;
        
        using (var db = new PasswordContext())
        {
            db.Database.Migrate();
        }   
    }
}