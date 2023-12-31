﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PasswordGenerator.Views;

public partial class PasswordsListWindow : Window
{
    public PasswordsListWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}