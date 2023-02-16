using System.ComponentModel.DataAnnotations;

namespace myshelter_api.Models
{
    public class enterprise
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Detail { get; set;  }

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public int Capacity { get; set; }
                
        public double Dimension { get; set; }

        public string ImageUrl { get; set; }

        public string Amenidad { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

    }
}
