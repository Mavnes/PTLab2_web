using System.ComponentModel.DataAnnotations;

namespace PTLab2_web.Data.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }

        public float UsedPrice { get; set; }

        public Purchase(int id, DateTime date, string address, string productName, float usedPrice)
        {
            Id = id;
            Date = date;
            Address = address;
            ProductName = productName;
            UsedPrice = usedPrice;
        }
    }
}
