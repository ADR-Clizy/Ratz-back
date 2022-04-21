using System.ComponentModel.DataAnnotations;

namespace Ratz_API.UserAggregate.DataTransferObjects
{
    public class UserConnectionDTO
    {
        public UserConnectionDTO() {}
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
