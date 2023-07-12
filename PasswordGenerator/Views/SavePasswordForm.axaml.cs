using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PasswordGenerator.ViewModels;

namespace PasswordGenerator.Views;

public partial class SavePasswordForm : Window
{
    public SavePasswordForm()
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