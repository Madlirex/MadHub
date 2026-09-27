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

public class InstallsViewModel : ViewModelBase
{
    public ObservableCollection<EditorManifestData> EditorManifests => EditorManifestDataManager.Datas;
    
}

public class DocsViewModel : ViewModelBase { }
