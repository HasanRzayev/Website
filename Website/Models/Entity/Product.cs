namespace Website.Models.Entity
{
    public class Product:Entity
    {
        public string image_url { get; set; }
        public string Name { get; set; }
        public int catalogue_id { get; set; }
        public int price { get; set; }
        public virtual Catalogue Catalogue { get; set; }

    }
}
