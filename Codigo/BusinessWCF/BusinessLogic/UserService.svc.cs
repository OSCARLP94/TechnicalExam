using BusinessWCF.DataAccess.ContextDB;
using BusinessWCF.DTOs;
using BusinessWCF.Entities;
using BusinessWCF.Mappers;
using BusinessWCF.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessWCF.BusinessLogic
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "UserService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione UserService.svc o UserService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepo;

        public UserService()
        {
            #region Hago instancia de BD aqui mienstras averiguo una mejor forma
            var contextDb = new TechnicalExamDBContext(ConfigurationManager.ConnectionStrings["TechnicalDBConnection"].ConnectionString);
            _userRepo = new Repository<User>(contextDb);
            #endregion
        }
        public async Task<bool> Delete(string id)
        {
            try
            {
                //se procede a borrar usando SP como pide la prueba

                var result = -1; //the sp return id of entity
                DbCommand cmd = _userRepo.GetDbCommand();
                var cmd2 = UserSPMapper.DeleteUser(cmd, id);
                result = await _userRepo.ExecuteSP(cmd2);

                if (result <= 0)
                    throw new ArgumentException("Error deleting user");

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UserDTO> Get(string id)
        {
            try
            {
                return UserDataMapper.FromUserToUserDTO(await _userRepo.Get(id));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UserDTO>> GetAll()
        {
            try
            {
                return UserDataMapper.FromUsersToUserDTOs(await _userRepo.List())?.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UserDTO> Register(UserDTO newUser)
        {
            try
            {

                //generamos id unico
                newUser.Id = Guid.NewGuid().ToString();

                //registramos en este caso usando SP como pide la prueba
                var result = -1; //the sp return id of entity
                DbCommand cmd = _userRepo.GetDbCommand();
                var cmd2 = UserSPMapper.RegisterUser(cmd, UserDataMapper.FromUserDTOToUser(newUser));
                result = await _userRepo.ExecuteSP(cmd2);

                if (result <= 0)
                    throw new ArgumentException("Error registering user");

                return newUser;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UserDTO> Update(UserDTO editUser)
        {
            try
            {
                User current = await _userRepo.Get(editUser.Id);

                if (current == null)
                    throw new ArgumentException($"User {editUser.Id} not exists");

                //registramos en este caso usando SP como pide la prueba
                var result = -1; //the sp return id of entity
                DbCommand cmd = _userRepo.GetDbCommand();
                var cmd2 = UserSPMapper.EditUser(cmd, UserDataMapper.FromUserDTOToUser(editUser));
                result = await _userRepo.ExecuteSP(cmd2);

                if (result <= 0)
                    throw new ArgumentException("Error updating user");

                return editUser;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
