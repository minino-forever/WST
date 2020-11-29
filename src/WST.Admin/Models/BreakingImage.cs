using System;

namespace WST.Admin.Models
{
    public class BreakingImage : BaseEntity
    {
        public Breaking Breaking { get; set; }

        public Guid BreakingId { get; set; }

        public byte[] Image { get; set; }
    }
}