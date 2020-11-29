using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WST.Admin.Models.ViewModels
{
    [SuppressMessage("ReSharper", "Mvc.TemplateNotResolved")]
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле 'Имя' обязательное")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Поле 'Пароль' обязательное")]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}