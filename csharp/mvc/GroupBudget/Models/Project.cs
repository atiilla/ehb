﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace GroupBudget.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Display (Name="Started")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Display (Name="Will end")]
        [Required]
        [DataType (DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.MaxValue;

        [Display (Name="Estimated Budget")]
        public Decimal EstimatedBudget { get; set; } = 0;

        [ForeignKey ("Category")]
        public int CategoryId { get; set; } = 1;


        public DateTime Deleted { get; set; } = DateTime.MaxValue;
        public Category? Category { get; set; }

    }
}
