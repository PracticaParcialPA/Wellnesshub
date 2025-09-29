using System;
using System.ComponentModel.DataAnnotations;

namespace PracticaPE.Models
{
    public class Meal
    {
        public int Id { get; set; }

        // Puedes cambiar a int si luego relacionas con tabla Users
        [Required, StringLength(100)]
        public string User { get; set; } = string.Empty;

        [Required]
        public DateTime EntryDate { get; set; }  // yyyy-MM-dd

        [Required, StringLength(200)]
        public string FoodName { get; set; } = string.Empty;

        [Range(0, 10000)]
        public int Calories { get; set; }

        [Range(0, 1000)]
        public float Protein { get; set; }

        [Range(0, 1000)]
        public float Carbs { get; set; }

        [Range(0, 1000)]
        public float Fat { get; set; }
    }
}