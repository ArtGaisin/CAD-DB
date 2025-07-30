using System.Windows;
using WPFUI.Services;
using WPFUI.ViewModels;

namespace WPFUI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var noteService = new NoteService();
            var mainViewModel = new MainViewModel(noteService);

            var mainWindow = new Views.MainWindow
            {
                DataContext = mainViewModel
            };

            mainWindow.Show();
        }
    }
}