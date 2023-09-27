using BusinessWCF.DTOs;
using BusinessWCF.Entities;
using System.Collections.Generic;

namespace BusinessWCF.Mappers
{
    public static class UserDataMapper
    {
        public static UserDTO FromUserToUserDTO(User user)
        {
            return new UserDTO() { Id = user.Id, Name = user.Name, BirthDate = user.BirthDate, Sex= user.Sex };
        }

        public static IEnumerable<UserDTO> FromUsersToUserDTOs(IEnumerable<User> users)
        {
            foreach (var user in users) 
                yield return FromUserToUserDTO(user);
        }

        public static User FromUserDTOToUser(UserDTO user)
        {
            return new User() { Id = user.Id, Name = user.Name, BirthDate = user.BirthDate, Sex = user.Sex };
        }
    }
}