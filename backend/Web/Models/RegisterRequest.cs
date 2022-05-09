namespace Web.Models
{
    public class RegisterRequest
    {
        public bool Agreed { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int CountryId { get; set; }

        public int? ProvinceId { get; set; }
    }
}
