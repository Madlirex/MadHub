using System.Windows.Input;

namespace MadHub;

public class MainViewModel : ViewModelBase
{
    private object _currentView;
    public object CurrentView
    {
        get => _currentView;
        set 
        { 
            _currentView = value; 
            OnPropertyChanged(); 
        }
    }
    
    public ProjectsViewModel ProjectsVm { get; } = new();
    public InstallsViewModel InstallsVm { get; } = new();
    public DocsViewModel DocsVm { get; } = new();
    
    public ICommand ShowProjectsCommand { get; }
    public ICommand ShowInstallsCommand { get; }
    public ICommand ShowDocsCommand { get; }

    public MainViewModel()
    {
        CurrentView = ProjectsVm;
        _currentView = ProjectsVm;
        
        ShowProjectsCommand = new RelayCommand(_ => CurrentView = ProjectsVm);
        ShowInstallsCommand = new RelayCommand(_ => CurrentView = InstallsVm);
        ShowDocsCommand = new RelayCommand(_ => CurrentView = DocsVm);
    }
}

public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    public RelayCommand(Action<object> execute) => _execute = execute;
    public bool CanExecute(object parameter) => true;
    public void Execute(object parameter) => _execute(parameter);
    public event EventHandler CanExecuteChanged;
}