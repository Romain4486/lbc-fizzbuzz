using System;
using System.ComponentModel.DataAnnotations;

namespace FizzBuzz.Domain.Entities
{
    public class FizzBuzzModel
    {
        [Required]
        [Range(1, 100)]
        public int FizzNumber { get; set; }

        [Required]
        [Range(1, 100)]
        public int BuzzNumber { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Limit { get; set; }

        [Required]
        [StringLength(20)]
        public string FizzLabel { get; set; }

        [Required]
        [StringLength(20)]
        public string BuzzLabel { get; set; }
    }
}
