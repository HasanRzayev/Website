namespace Website.Models.Entity
{
    public class Catalogue:Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
