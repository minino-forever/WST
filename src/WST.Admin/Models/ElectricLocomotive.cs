using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WST.Admin.Models
{
    public class ElectricLocomotive : BaseEntity
    {
        [Required(ErrorMessage = "Пожалуйста, введите модификацию")]
        public string Modification { get; set; }
        
        [Required(ErrorMessage = "Пожалуйста, введите серийный номер")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите уникальный номер")]
        public string UniqueNumber { get; set; }    
        
        [Required]
        [Range(3000, 15000, ErrorMessage = "Пожалуйста, введите мощность")]
        public int Power { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "Пожалуйста, введите количество секций")]
        public int SectionCount { get; set; }
        
        [Required]
        [Range(4, 16, ErrorMessage = "Пожалуйста, введите количество осей")]
        public int PinCount { get; set; }
        
        public DateTime CreateDate { get; set; }

        public ICollection<ElectricLocomotiveBreakingProxy> LocomotiveBreakingProxies { get; set; }
    }
}