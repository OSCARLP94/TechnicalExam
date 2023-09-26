using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace TechnicalExamT3.Models
{
    [DataContract]
    public class UserViewModel
    {
        public string? Id { get; set; }

        /// <summary>
        /// Nombre usuario
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Fecha nacimiento
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Sexo
        /// </summary>
        [Required]
        public string Sex { get; set; }
    }

    public class UserActionViewModel
    {
        /// <summary>
        /// Accion transversal
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Id de usuario
        /// </summary>
        public string? Id { get; set; }
    }

    public class UserCompositeViewModel
    {
        public UserViewModel User { get; set; }
        public UserActionViewModel Action { get; set; }
    }

    public class UserListViewModel
    {
        public List<UserViewModel> Users { get; set; }
    }
}
