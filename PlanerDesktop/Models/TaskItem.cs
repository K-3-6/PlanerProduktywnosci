using System;

namespace PlanerDesktop.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = "Praca";
        public string Priority { get; set; } = "Średni";
        public DateTime DueDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Nowe";
    }
}