using System;
using System.ComponentModel.DataAnnotations;

namespace CarInsurance.Models
{
    public class Insuree
    {
        public int Id { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string EmailAddress { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int CarYear { get; set; }

        [Required]
        public required string CarMake { get; set; }

        [Required]
        public string? CarModel { get; set; }

        public bool DUI { get; set; }

        public int SpeedingTickets { get; set; }

        [Required]
        public required string CoverageType { get; set; }

        public decimal Quote { get; set; }
    }
}