namespace Ratz_API.Models
{
    public class UserTokens
    {
        public string Token { get; set; }
        public TimeSpan Validaty { get; set; }
        public string RefreshToken { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public Guid GuidId { get; set; }
        public DateTime ExpiredTime { get; set; }

    }
}
