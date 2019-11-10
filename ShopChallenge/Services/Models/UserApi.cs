using System.ComponentModel.DataAnnotations;

namespace ShopChallenge.Repositories.Models
{
    public class UserApi
    {
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Token { get; set; }

        public PointApi Coordinates { get; set; }
    }
}
