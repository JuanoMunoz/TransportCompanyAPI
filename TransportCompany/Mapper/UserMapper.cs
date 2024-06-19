using TransportCompany.Dto_s.Users;
using TransportCompany.Models;

namespace TransportCompany.Mapper
{
    public static class UserMapper
    {
        public static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO
            {
                Username = user.UserName,
                Salary = user.Salary,
                BirthDate = user.BirthDate,
                BranchId = user.BranchId,
                JoiningDate = user.JoiningDate,
      
            };
        }
        public static UserMessageDTO ToUserMessageDTO(this User user)
        {
            return new UserMessageDTO
            {
                Username = user.UserName!,
                Email = user.Email!
                
            };
        }
    }
}
