using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using PlanerMobile.Models;

namespace PlanerMobile
{
    public partial class MainPage : ContentPage
    {
        private readonly string apiUrl = "http://10.0.2.2:5216/api/tasks";
        private readonly HttpClient client = new HttpClient();

        public ICommand DeleteTaskCommand { get; private set; }

        public MainPage()
        {
            InitializeComponent();

            // Definiujemy akcję usuwania
            DeleteTaskCommand = new Command<TaskItem>(ExecuteDeleteTask);
            BindingContext = this;

            LoadTasks();
        }

        // 1. Pobieranie danych z bazy API
        private async void LoadTasks()
        {
            try
            {
                var tasks = await client.GetFromJsonAsync<List<TaskItem>>(apiUrl);
                ColTasks.ItemsSource = tasks;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", $"Nie udało się pobrać danych z API: {ex.Message}", "OK");
            }
        }

        // 2. Odświeżanie danych
        private void BtnRefresh_Clicked(object sender, EventArgs e)
        {
            LoadTasks();
        }

        // 3. Dodawanie nowego zadania z poziomu telefonu
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntTitle.Text))
            {
                await DisplayAlert("Ostrzeżenie", "Tytuł zadania nie może być pusty!", "OK");
                return;
            }

            var newTask = new TaskItem
            {
                Title = EntTitle.Text,
                Description = EntDescription.Text,
                Category = "Mobilne", // Automatyczna flaga dla zadań z telefonu
                Priority = "Średni",
                DueDate = DateTime.Now,
                Status = "Nowe"
            };

            try
            {
                var response = await client.PostAsJsonAsync(apiUrl, newTask);
                if (response.IsSuccessStatusCode)
                {
                    EntTitle.Text = string.Empty;
                    EntDescription.Text = string.Empty;
                    LoadTasks(); // Odśwież widok
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", $"Błąd dodawania: {ex.Message}", "OK");
            }
        }

        // 4. Usuwanie zadania
        private async void ExecuteDeleteTask(TaskItem task)
        {
            if (task == null) return;

            bool confirm = await DisplayAlert("Potwierdzenie", $"Czy chcesz usunąć zadanie: {task.Title}?", "Tak", "Nie");
            if (confirm)
            {
                try
                {
                    var response = await client.DeleteAsync($"{apiUrl}/{task.Id}");
                    if (response.IsSuccessStatusCode)
                    {
                        LoadTasks();
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Błąd", $"Błąd usuwania: {ex.Message}", "OK");
                }
            }
        }
    }
}