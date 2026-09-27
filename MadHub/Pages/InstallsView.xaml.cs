using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace MadHub;

public partial class InstallsView : UserControl
{
    public InstallsView()
    {
        InitializeComponent();
    }
    
    private void LocateEditor_Click(object sender, RoutedEventArgs e)
    {
        var folderDialog = new OpenFolderDialog
        {
            Title = "Select Editor Installation Folder",
        };
        
        if (folderDialog.ShowDialog() != true) return;
        string selectedPath = folderDialog.FolderName;
        
        EditorManifestDataManager.Add(selectedPath);
    }
    private void MenuButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.ContextMenu == null) return;
        button.ContextMenu.PlacementTarget = button;
        button.ContextMenu.IsOpen = true;
    }
    
    private void OpenInExplorer_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem { DataContext: EditorManifestData installData }) return;
        
        if (!string.IsNullOrEmpty(installData.Path) && File.Exists(installData.Path))
        {
            string directory = Path.GetDirectoryName(installData.Path)!;
            if (Directory.Exists(directory))
            {
                Process.Start("explorer.exe", directory);
            }
        }
        else if (Directory.Exists(installData.Path))
        {
            Process.Start("explorer.exe", installData.Path);
        }
    }

    private void RemoveInstall_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem menuItem || menuItem.DataContext is not EditorManifestData installData) return;
        
        EditorManifestDataManager.Remove(installData);
    }
}