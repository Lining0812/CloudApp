using CloudEFCore.Models;

namespace CloudWebApi.Models
{
    public class UserDto
    {
        public string Name { get; set; }

        public UserDto()
        {
        }
        public UserDto(User user)
        {
            Name = user.Name;
        }
    }
}
