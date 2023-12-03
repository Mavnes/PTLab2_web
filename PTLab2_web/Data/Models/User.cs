using System.ComponentModel.DataAnnotations;

namespace PTLab2_web.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        public float TotalExpenses { get; set; }
        public float Sale { get; set; }

        public User(int id, string name, string email, string password, float totalExpenses, float sale)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            TotalExpenses = totalExpenses;
            Sale = sale;
        }

        public User(string name, string email, string password)
        {
            Id = 0;
            Name = name;
            Email = email;
            Password = password;
            TotalExpenses = 0;
            Sale = 0;
        }
        public User()
        {
            Id = 0;
            Name = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            TotalExpenses = 0;
            Sale = 0;
        }
    }
}
