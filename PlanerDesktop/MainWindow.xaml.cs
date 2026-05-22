using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using PlanerDesktop.Models;

namespace PlanerDesktop
{
    public partial class MainWindow : Window
    {
        // Adres URL Twojego działającego API z Etapu 2 (Upewnij się, że port w localhost jest poprawny!)
        private readonly string apiUrl = "http://localhost:5000/api/tasks";
        private readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            LoadTasksFromApi();
        }

        // 1. Pobieranie danych z bazy przez API
        private async void LoadTasksFromApi()
        {
            try
            {
                var tasks = await client.GetFromJsonAsync<List<TaskItem>>(apiUrl);
                DgTasks.ItemsSource = tasks;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd pobierania danych: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 2. Obsługa przycisku: Odśwież
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadTasksFromApi();
        }

        // 3. Obsługa przycisku: Dodaj zadanie
        private async void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtTitle.Text))
            {
                MessageBox.Show("Pole 'Tytuł' nie może być puste!", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newTask = new TaskItem
            {
                Title = TxtTitle.Text,
                Description = TxtDescription.Text,
                Category = ((ComboBoxItem)CmbCategory.SelectedItem).Content.ToString()!,
                Priority = ((ComboBoxItem)CmbPriority.SelectedItem).Content.ToString()!,
                DueDate = DateTime.Now,
                Status = "Nowe"
            };

            try
            {
                var response = await client.PostAsJsonAsync(apiUrl, newTask);
                if (response.IsSuccessStatusCode)
                {
                    TxtTitle.Clear();
                    TxtDescription.Clear();
                    LoadTasksFromApi(); // Przeładuj tabelę
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas dodawania: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 4. Obsługa przycisku: Usuń zaznaczone zadanie
        private async void BtnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var selectedTask = DgTasks.SelectedItem as TaskItem;
            if (selectedTask == null)
            {
                MessageBox.Show("Wybierz zadanie z tabeli, które chcesz usunąć.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show($"Czy na pewno chcesz usunąć zadanie: {selectedTask.Title}?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var response = await client.DeleteAsync($"{apiUrl}/{selectedTask.Id}");
                    if (response.IsSuccessStatusCode)
                    {
                        LoadTasksFromApi();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas usuwania: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}