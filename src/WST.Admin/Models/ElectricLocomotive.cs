using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WST.Admin.Models
{
    public class ElectricLocomotive : BaseEntity
    {
        public string Modification { get; set; }
        
        public string SerialNumber { get; set; }

        public string UniqueNumber { get; set; }    
        
        public int Power { get; set; }

        public int SectionCount { get; set; }
        
        public int PinCount { get; set; }
        
        public DateTime CreateDate { get; set; }

        public ICollection<ElectricLocomotiveBreakingProxy> LocomotiveBreakingProxies { get; set; }
    }
}