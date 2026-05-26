using System;
using System.ComponentModel.DataAnnotations;

namespace PlanerAPI.Models
{
    public class TaskItem
    {
        [Key] // Klucz główny (ID autoinkrementowane)
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = "Praca"; // Praca, Dom, Nauka itd.

        [Required]
        public string Priority { get; set; } = "Średni"; // Niski, Średni, Wysoki

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string Status { get; set; } = "Nowe"; // Nowe, W trakcie, Zakończone
    }
}