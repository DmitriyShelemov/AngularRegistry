namespace Domain
{
    public class Province
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}