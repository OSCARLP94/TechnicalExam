using TechnicalExamBlazor.Data.Mappers;
using TechnicalExamBlazor.Data.Models;
using UserServiceWCF;

namespace TechnicalExamBlazor.Data
{
    public class UserService
    {
        private readonly IUserService _userService;
        public UserService(IUserService userService) 
        { 
            _userService = userService;
        }

        #region Methods
        public async Task<IEnumerable<UserViewModel>> GetListUsers()
        {
            try
            {
                return UserMapper.FromUsersDTOToUsersViewModel(await _userService.GetAllAsync());
            }catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<UserViewModel> GetUser(string id)
        {
            try 
            {
                return UserMapper.FromUserDTOToUserViewModel(await _userService.GetAsync(id));
            }catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> RegisterUser(UserViewModel user)
        {
            try
            {
                await _userService.RegisterAsync(UserMapper.FromUserViewModelToUserDTO(user));
                NotifyUserUpdated();
                return true;
            }catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateUser(UserViewModel user)
        {
            try
            {
                await _userService.UpdateAsync(UserMapper.FromUserViewModelToUserDTO(user));
                NotifyUserUpdated();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteUser(string id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                NotifyUserUpdated();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public event Func<Task> OnUserUpdated;

        /// <summary>
        /// Metodo para invocar evento de actualizacion de usuario hacia suscriptores
        /// </summary>
        private async Task NotifyUserUpdated()=> await OnUserUpdated?.Invoke();
        #endregion
    }
}
