using Business.Entities;
using TechnicalExamT3.Models;
using UserServiceWCF;

namespace TechnicalExamT3.Mappers
{
    public static class UserMapper
    {
        /// <summary>
        /// Convierte lista de usuarios a lista usarios viewmodel
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public static IEnumerable<UserViewModel> FromUsersToViewModels(List<User> users)
        {
            foreach (var user in users)
                yield return FromUserToUserViewModel(user);

        }

        public static UserViewModel FromUserDTOToUserViewModel(UserDTO user)
        {
            return new UserViewModel { Id = user.Id, BirthDate=user.BirthDate, Name = user.Name, Sex = user.Sex };
        }

        public static IEnumerable<UserViewModel> FromUsersDTOToUsersViewModel(UserDTO[] users)
        {
            foreach (var user in users)
                yield return FromUserDTOToUserViewModel(user);
        }

        public static User FromUserViewModelToUser(UserViewModel model)
        {
            return new User { Id = model.Id, Name = model.Name, BirthDate = model.BirthDate, Sex = model.Sex };
        }

        public static UserDTO FromUserViewModelToUserDTO(UserViewModel model)
        {
            return new UserDTO { Id = model.Id, Name = model.Name, BirthDate = model.BirthDate, Sex = model.Sex };
        }

        public static UserViewModel FromUserToUserViewModel(User user)
        {
            return new UserViewModel { Id = user.Id, Name = user.Name, BirthDate = user.BirthDate, Sex = user.Sex };
        }
    }
}
