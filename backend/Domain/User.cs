namespace Domain
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool Agreed { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public int? ProvinceId { get; set; }

        public virtual Province Province { get; set; }

    }
}