
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace WPFUI
{
    public partial class MainWindow : Window
    {
        private const string ApiBaseUrl = "https://localhost:7197/Note";
        private readonly HttpClient _httpClient;

        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void CreateNote_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CreateNoteTextBox.Text))
            {
                MessageBox.Show("Please enter note text", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {                                
                string noteText = CreateNoteTextBox.Text;               
                string jsonContent = JsonSerializer.Serialize(noteText);                
                var content = new StringContent(
                    jsonContent,
                    Encoding.UTF8,
                    "application/json");               

                var response = await _httpClient.PostAsync(ApiBaseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Note created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    CreateNoteTextBox.Clear();
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

        private async void GetNote_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ViewNoteIdTextBox.Text, out var id))
            {
                MessageBox.Show("Please enter a valid note ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var response = await _httpClient.GetAsync($"{ApiBaseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var noteText = await response.Content.ReadAsStringAsync();
                    ViewNoteResultTextBox.Text = noteText;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Note not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ViewNoteResultTextBox.Clear();
                }
                else
                {
                    MessageBox.Show($"Failed to get note. Status code: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ViewNoteResultTextBox.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ViewNoteResultTextBox.Clear();
            }
        }

        private async void UpdateNote_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(UpdateNoteIdTextBox.Text, out var id))
            {
                MessageBox.Show("Please enter a valid note ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(UpdateNoteTextBox.Text))
            {
                MessageBox.Show("Please enter new note text", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var content = new StringContent($"\"{UpdateNoteTextBox.Text}\"", Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{ApiBaseUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Note updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateNoteIdTextBox.Clear();
                    UpdateNoteTextBox.Clear();
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

        private async void DeleteNote_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(DeleteNoteIdTextBox.Text, out var id))
            {
                MessageBox.Show("Please enter a valid note ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete note #{id}?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{ApiBaseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Note deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    DeleteNoteIdTextBox.Clear();
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