using System;
using System.Collections.Generic;

namespace WST.Admin.Models
{
    /// <summary>Поломка</summary>
    public class Breaking : BaseEntity
    {
        /// <summary>Описание поломки</summary>
        public string Description { get; set; }

        /// <summary>Способ исправления поломки</summary>
        public string RepairMethod { get; set; }

        /// <summary>Дата создания</summary>
        public DateTime CreateDate { get; set; }
        
        /// <summary>Связь тепловоза с возможными поломками</summary>
        public ICollection<ElectricLocomotiveBreakingProxy> LocomotiveBreakingProxies { get; set; }
    }
}