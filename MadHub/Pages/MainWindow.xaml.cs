using System.Windows;
using System.Windows.Input;

namespace MadHub;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        EditorManifestDataManager.Load();
        ProjectInfoManager.Load();
        InitializeComponent();
        DataContext = new MainViewModel();
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}