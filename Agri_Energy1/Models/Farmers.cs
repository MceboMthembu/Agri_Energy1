using System.ComponentModel.DataAnnotations;

namespace Agri_Energy1.Models
{
    public class Farmers
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }    

        public string Password { get; set; }

        public ICollection<FarmerProducts> FarmerProducts { get; set; } = new List<FarmerProducts>();
    }

}
