using System;
using System.ComponentModel.DataAnnotations;

namespace WST.Admin.Models.ViewModels.Breaking
{
    public class BreakingFormDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите описание")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Пожалуйста, введите способ устранения")]
        public string RepairMethod { get; set; }

        public string[] ImageUrls { get; set; } = new string[0];
    }
}