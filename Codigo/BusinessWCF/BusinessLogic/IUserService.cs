using BusinessWCF.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessWCF.BusinessLogic
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IUserService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IUserService
    {
        /// <summary>
        /// Devuelve lista de usuarios
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Task<List<UserDTO>> GetAll();

        /// <summary>
        /// Registra usuario
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [OperationContract]
        Task<UserDTO> Register(UserDTO newUser);

        /// <summary>
        /// Actualiza usuario
        /// </summary>
        /// <param name="editUser"></param>
        /// <returns></returns>
        [OperationContract]
        Task<UserDTO> Update(UserDTO editUser);

        /// <summary>
        /// Elimina usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Task<bool> Delete(String id);
    }
}
