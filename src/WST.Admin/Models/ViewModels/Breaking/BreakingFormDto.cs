using System;

namespace WST.Admin.Models.ViewModels.Breaking
{
    public class BreakingFormDto
    {
        public Guid Id { get; set; }

        public string Description { get; set; }
        
        public string RepairMethod { get; set; }

        public string[] ImageUrls { get; set; }
    }
}