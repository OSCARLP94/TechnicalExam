using System.ComponentModel.DataAnnotations;

namespace TechnicalExamBlazor.Data.Models
{
    public class SessionViewModel
    {
        [Required]
        [MinLength(6, ErrorMessage ="Se requiere minimo 6 caracteres")]
        public string UserName {  get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Se requiere minimo 6 caracteres")]
        public string Password { get; set; }
    }
}
