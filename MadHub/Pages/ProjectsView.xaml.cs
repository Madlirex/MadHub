using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace MadHub;

public partial class ProjectsView : UserControl
{
    public ProjectsView()
    {
        InitializeComponent();
    }
    
    private void LocateProject_Click(object sender, RoutedEventArgs e)
    {
        var folderDialog = new OpenFolderDialog
        {
            Title = "Select Project Folder",
        };
        
        if (folderDialog.ShowDialog() != true) return;
        string selectedPath = folderDialog.FolderName;
        
        ProjectInfoManager.Add(selectedPath);
    }
    private void MenuButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.ContextMenu == null) return;
        button.ContextMenu.PlacementTarget = button;
        button.ContextMenu.IsOpen = true;
    }
    
    private void OpenInExplorer_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem { DataContext: ProjectInfo project }) return;
        
        if (!string.IsNullOrEmpty(project.Path) && File.Exists(project.Path))
        {
            string directory = Path.GetDirectoryName(project.Path)!;
            if (Directory.Exists(directory))
            {
                Process.Start("explorer.exe", directory);
            }
        }
        else if (Directory.Exists(project.Path))
        {
            Process.Start("explorer.exe", project.Path);
        }
    }

    private void RemoveProject_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem { DataContext: ProjectInfo projectInfo }) return;

        ProjectInfoManager.Remove(projectInfo);
    }
    
    private void ProjectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ListBox { SelectedItem: ProjectInfo selectedProject } listBox) return;
        ProjectInfoManager.OpenProject(selectedProject);
            
        listBox.SelectedIndex = -1;
    }
}