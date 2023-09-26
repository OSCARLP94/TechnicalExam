using Business.Entities;
using TechnicalExamT3.Models;

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
                yield return new UserViewModel { Id = user.Id, Name = user.Name, BirthDate = user.BirthDate, Sex = user.Sex };

        }

        public static User FromUserViewModelToUser(UserViewModel model)
        {
            return new User { Id = model.Id, Name = model.Name, BirthDate = model.BirthDate, Sex = model.Sex };
        }

        public static UserViewModel FromUserToUserViewModel(User user)
        {
            return new UserViewModel { Id = user.Id, Name = user.Name, BirthDate = user.BirthDate, Sex = user.Sex };
        }
    }
}
