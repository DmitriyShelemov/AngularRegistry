namespace Domain
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Province> Provinces { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}