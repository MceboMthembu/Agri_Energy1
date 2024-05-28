using System.ComponentModel.DataAnnotations;

namespace Agri_Energy1.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime ProductionDate { get; set; }
        public ICollection<FarmerProducts> FarmerProducts { get; set; } = new List<FarmerProducts>();

      

    }

    public class FarmerProducts
    {
        [Key]
        public int FarmerId { get; set; }
        public Farmers Farmers { get; set; }
        [Key]
        public int ProductId { get; set; }
        public Products Products { get; set; }



    }
}
