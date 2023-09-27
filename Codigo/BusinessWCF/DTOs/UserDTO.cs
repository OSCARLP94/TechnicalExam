using Microsoft.Build.Framework;
using System;

namespace BusinessWCF.DTOs
{
    public class UserDTO
    {
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Nombre de usuario
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Sexo
        /// </summary>
        [Required]
        public string Sex { get; set; }

    }
}