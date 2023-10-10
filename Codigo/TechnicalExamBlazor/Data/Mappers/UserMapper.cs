using TechnicalExamBlazor.Data.Models;
using UserServiceWCF;

namespace TechnicalExamBlazor.Data.Mappers
{
    public static class UserMapper
    {
        /// <summary>
        /// Convierte lista de usuarios a lista usarios viewmodel
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>

        public static UserViewModel FromUserDTOToUserViewModel(UserDTO user)
        {
            return new UserViewModel { Id = user.Id, BirthDate=user.BirthDate, Name = user.Name, Sex = user.Sex };
        }

        public static IEnumerable<UserViewModel> FromUsersDTOToUsersViewModel(UserDTO[] users)
        {
            foreach (var user in users)
                yield return FromUserDTOToUserViewModel(user);
        }

        public static UserDTO FromUserViewModelToUserDTO(UserViewModel model)
        {
            return new UserDTO { Id = model.Id, Name = model.Name, BirthDate = model.BirthDate, Sex = model.Sex };
        }

    }
}
