using System;

namespace WST.Admin.Models
{
    /// <summary>Пользователь</summary>
    public class User : BaseEntity
    {
        /// <summary>Email</summary>
        public string Email { get; set; }

        /// <summary>Password</summary>
        public string Password { get; set; }
        
        /// <summary>Имя</summary>
        public string Name { get; set; }

        /// <summary>Фамилия</summary>
        public string Surname { get; set; }
        
        /// <summary>Отчество</summary>
        public string Patronym { get; set; }
        
        /// <summary>Дата создания</summary>
        public DateTime CreateDate { get; set; }
    }
}