using System;
using Microsoft.AspNetCore.Http;

namespace WST.Admin.Models.ViewModels.Breaking
{
    public class BreakingFormDto
    {
        public Guid Id { get; set; }

        public string Description { get; set; }
        
        public string RepairMethod { get; set; }
        
        public IFormFile File { get; set; }
    }
}