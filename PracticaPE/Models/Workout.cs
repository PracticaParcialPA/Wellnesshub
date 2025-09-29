using System;
using System.ComponentModel.DataAnnotations;

namespace PracticaPE.Models
{
    public class Workout
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime SessionDate { get; set; }

        [Required, MaxLength(100)]
        public string Exercise { get; set; }

        [Required]
        public int DurationMinutes { get; set; }

        [MaxLength(50)]
        public string? Intensity { get; set; }

        public int? CaloriesBurned { get; set; }
    }
}