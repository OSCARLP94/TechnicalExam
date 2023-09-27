using System;

namespace BusinessWCF.Entities
{
    public class User
    {
        public string Id { get; set; }

        /// <summary>
        /// Nombre de usuario
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Sexo
        /// </summary>
        public string Sex { get; set; }
    }
}
