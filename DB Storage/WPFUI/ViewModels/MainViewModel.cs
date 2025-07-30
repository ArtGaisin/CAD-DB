using System.Windows;
using System.Windows.Input;
using WPFUI.Services;

namespace WPFUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INoteService _noteService;

        private string _createNoteText;
        private int _viewNoteId;
        private string _viewNoteResult;
        private int _updateNoteId;
        private string _updateNoteText;
        private int _deleteNoteId;

        public string CreateNoteText
        {
            get => _createNoteText;
            set { _createNoteText = value; OnPropertyChanged(); }
        }

        public int ViewNoteId
        {
            get => _viewNoteId;
            set { _viewNoteId = value; OnPropertyChanged(); }
        }

        public string ViewNoteResult
        {
            get => _viewNoteResult;
            set { _viewNoteResult = value; OnPropertyChanged(); }
        }

        public int UpdateNoteId
        {
            get => _updateNoteId;
            set { _updateNoteId = value; OnPropertyChanged(); }
        }

        public string UpdateNoteText
        {
            get => _updateNoteText;
            set { _updateNoteText = value; OnPropertyChanged(); }
        }

        public int DeleteNoteId
        {
            get => _deleteNoteId;
            set { _deleteNoteId = value; OnPropertyChanged(); }
        }

        public ICommand CreateNoteCommand { get; }
        public ICommand GetNoteCommand { get; }
        public ICommand UpdateNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }
        public MainViewModel() : this(new NoteService())
        {
        }
        public MainViewModel(INoteService noteService)
        {
            _noteService = noteService;

            CreateNoteCommand = new RelayCommand(async _ => await CreateNoteAsync());
            GetNoteCommand = new RelayCommand(async _ => await GetNoteAsync());
            UpdateNoteCommand = new RelayCommand(async _ => await UpdateNoteAsync());
            DeleteNoteCommand = new RelayCommand(async _ => await DeleteNoteAsync());
        }

        private async Task CreateNoteAsync()
        {
            if (string.IsNullOrWhiteSpace(CreateNoteText))
            {
                MessageBox.Show("Please enter note text", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var response = await _noteService.CreateNoteAsync(CreateNoteText);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Note created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    CreateNoteText = string.Empty;
                }
                else
                {
                    MessageBox.Show($"Failed to create note. Status code: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task GetNoteAsync()
        {
            try
            {
                var response = await _noteService.GetNoteAsync(ViewNoteId);

                if (response.IsSuccessStatusCode)
                {
                    ViewNoteResult = await response.Content.ReadAsStringAsync();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Note not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ViewNoteResult = string.Empty;
                }
                else
                {
                    MessageBox.Show($"Failed to get note. Status code: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ViewNoteResult = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ViewNoteResult = string.Empty;
            }
        }

        private async Task UpdateNoteAsync()
        {
            if (string.IsNullOrWhiteSpace(UpdateNoteText))
            {
                MessageBox.Show("Please enter new note text", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var response = await _noteService.UpdateNoteAsync(UpdateNoteId, UpdateNoteText);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Note updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateNoteId = 0;
                    UpdateNoteText = string.Empty;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Note not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Failed to update note. Status code: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task DeleteNoteAsync()
        {
            if (MessageBox.Show($"Are you sure you want to delete note #{DeleteNoteId}?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                var response = await _noteService.DeleteNoteAsync(DeleteNoteId);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Note deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    DeleteNoteId = 0;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Note not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Failed to delete note. Status code: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}