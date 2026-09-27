using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MadHub;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


public class ProjectsViewModel : ViewModelBase { }

public class EditorInstall
{
    public string Version { get; set; }
    public string Path { get; set; }
    public bool IsLts { get; set; }
    public List<string> Platforms { get; set; } = new List<string>();
}

public class InstallsViewModel : ViewModelBase
{
    public ObservableCollection<EditorInstall> InstalledVersions { get; set; }

    public InstallsViewModel()
    {
        InstalledVersions = [];
    }
}

public class DocsViewModel : ViewModelBase { }
