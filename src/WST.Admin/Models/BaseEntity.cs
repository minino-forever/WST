using System;

namespace WST.Admin.Models
{
    /// <summary>Базовая сущность</summary>
    public abstract class BaseEntity
    {
        /// <summary>Идентификатор</summary>
        public Guid Id { get; set; }
    }
}