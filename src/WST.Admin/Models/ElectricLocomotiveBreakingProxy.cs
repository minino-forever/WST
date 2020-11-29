using System;

namespace WST.Admin.Models
{
    /// <summary>Связь между тепловозом и поломкой</summary>
    public class ElectricLocomotiveBreakingProxy : BaseEntity
    {
        /// <summary>Тепловоз</summary>
        public ElectricLocomotive ElectricLocomotive { get; set; }

        /// <summary>Id тепловоза</summary>
        public Guid ElectricLocomotiveId { get; set; }

        /// <summary>Поломка</summary>
        public Breaking Breaking { get; set; }

        /// <summary>Id поломки</summary>
        public Guid BreakingId { get; set; }
    }
}