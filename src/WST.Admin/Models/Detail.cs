using System.ComponentModel.DataAnnotations;

namespace WST.Admin.Models
{
    /// <summary>Деталь</summary>
    public class Detail : BaseEntity
    {
        /// <summary>Наименование</summary>
        [Required(ErrorMessage = "Введите наименование")]
        public string Name { get; set; }    
        
        /// <summary>Описание</summary>
        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; }

        /// <summary>Идентификационный номер</summary>
        [Required(ErrorMessage = "Введите номер")]
        public string Number { get; set; }

        /// <summary>Количество на складе</summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Введите количество на складе")]
        public int Amount { get; set; }    
    }
}