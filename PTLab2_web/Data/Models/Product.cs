using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PTLab2_web.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public float Price { get; set; }

        public Product(int id, string name, float price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public Product()
        {
            Id = 0;
            Name = string.Empty;
            Price = 0;
        }
    }
}
