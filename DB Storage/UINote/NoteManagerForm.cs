using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UINote
{
    public partial class NoteManagerForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private const string ApiBaseUrl = "https://localhost:7197"; // Замените на ваш URL API
        private readonly HttpClient _httpClient;

        public NoteManagerForm(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
            _httpClient = new HttpClient();
            SetupControls();
        }

        private void SetupControls()
        {
            // Настройка формы
            this.Text = "Note Manager";
            this.Width = 600;
            this.Height = 700;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Создание вкладок
            var tabControl = new TabControl { Dock = DockStyle.Fill };
            this.Controls.Add(tabControl);

            // Вкладка создания заметки
            var createTab = new TabPage("Create Note");
            tabControl.TabPages.Add(createTab);
            SetupCreateTab(createTab);

            // Вкладка просмотра заметки
            var viewTab = new TabPage("View Note");
            tabControl.TabPages.Add(viewTab);
            SetupViewTab(viewTab);

            // Вкладка обновления заметки
            var updateTab = new TabPage("Update Note");
            tabControl.TabPages.Add(updateTab);
            SetupUpdateTab(updateTab);

            // Вкладка удаления заметки
            var deleteTab = new TabPage("Delete Note");
            tabControl.TabPages.Add(deleteTab);
            SetupDeleteTab(deleteTab);
        }

        private void SetupCreateTab(TabPage tab)
        {
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill };
            tab.Controls.Add(panel);

            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            // Текстовая область
            var label = new Label { Text = "Enter note text:", Dock = DockStyle.Fill };
            panel.Controls.Add(label, 0, 0);

            var textBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical
            };
            panel.Controls.Add(textBox, 0, 1);

            // Кнопка
            var button = new Button
            {
                Text = "Create Note",
                Dock = DockStyle.Fill
            };
            button.Click += async (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    MessageBox.Show("Please enter note text", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    var content = new StringContent($"\"{textBox.Text}\"", Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(ApiBaseUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Note created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox.Clear();
                    }
                    else
                    {
                        MessageBox.Show($"Failed to create note. Status code: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            panel.Controls.Add(button, 0, 2);
        }

        private void SetupViewTab(TabPage tab)
        {
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill };
            tab.Controls.Add(panel);

            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            // Поле для ID
            var idLabel = new Label { Text = "Note ID:", Dock = DockStyle.Fill };
            panel.Controls.Add(idLabel, 0, 0);

            var idTextBox = new TextBox { Dock = DockStyle.Fill };
            panel.Controls.Add(idTextBox, 0, 1);

            // Текстовая область для результата
            var resultTextBox = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical
            };
            panel.Controls.Add(resultTextBox, 0, 2);

            // Кнопка
            var button = new Button
            {
                Text = "Get Note",
                Dock = DockStyle.Fill
            };
            button.Click += async (sender, e) =>
            {
                if (!int.TryParse(idTextBox.Text, out var id))
                {
                    MessageBox.Show("Please enter a valid note ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    var response = await _httpClient.GetAsync($"{ApiBaseUrl}/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var noteText = await response.Content.ReadAsStringAsync();
                        resultTextBox.Text = noteText;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("Note not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        resultTextBox.Clear();
                    }
                    else
                    {
                        MessageBox.Show($"Failed to get note. Status code: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        resultTextBox.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error getting note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    resultTextBox.Clear();
                }
            };
            panel.Controls.Add(button, 0, 3);
        }

        private void SetupUpdateTab(TabPage tab)
        {
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill };
            tab.Controls.Add(panel);

            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            // Поле для ID
            var idLabel = new Label { Text = "Note ID:", Dock = DockStyle.Fill };
            panel.Controls.Add(idLabel, 0, 0);

            var idTextBox = new TextBox { Dock = DockStyle.Fill };
            panel.Controls.Add(idTextBox, 0, 1);

            // Текстовая область для нового текста
            var textLabel = new Label { Text = "New text:", Dock = DockStyle.Fill };
            panel.Controls.Add(textLabel, 0, 2);

            var textBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical
            };
            panel.Controls.Add(textBox, 0, 3);

            // Кнопка
            var button = new Button
            {
                Text = "Update Note",
                Dock = DockStyle.Fill
            };
            button.Click += async (sender, e) =>
            {
                if (!int.TryParse(idTextBox.Text, out var id))
                {
                    MessageBox.Show("Please enter a valid note ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    MessageBox.Show("Please enter new note text", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    var content = new StringContent($"\"{textBox.Text}\"", Encoding.UTF8, "application/json");
                    var response = await _httpClient.PutAsync($"{ApiBaseUrl}/{id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Note updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        idTextBox.Clear();
                        textBox.Clear();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("Note not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Failed to update note. Status code: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            panel.Controls.Add(button, 0, 4);
        }

        private void SetupDeleteTab(TabPage tab)
        {
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill };
            tab.Controls.Add(panel);

            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            // Поле для ID
            var idLabel = new Label { Text = "Note ID:", Dock = DockStyle.Fill };
            panel.Controls.Add(idLabel, 0, 0);

            var idTextBox = new TextBox { Dock = DockStyle.Fill };
            panel.Controls.Add(idTextBox, 0, 1);

            // Кнопка
            var button = new Button
            {
                Text = "Delete Note",
                Dock = DockStyle.Fill
            };
            button.Click += async (sender, e) =>
            {
                if (!int.TryParse(idTextBox.Text, out var id))
                {
                    MessageBox.Show("Please enter a valid note ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show($"Are you sure you want to delete note #{id}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                try
                {
                    var response = await _httpClient.DeleteAsync($"{ApiBaseUrl}/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Note deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        idTextBox.Clear();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("Note not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Failed to delete note. Status code: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            panel.Controls.Add(button, 0, 2);
        }
    }
}
