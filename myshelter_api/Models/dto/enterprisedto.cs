using System.ComponentModel.DataAnnotations;

namespace myshelter_api.Models.dto
{
    public class enterprisedto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Detail { get; set; }

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public int Capacity { get; set; }

        public double Dimension { get; set; }

        public string ImageUrl { get; set; }

        public string Amenidad { get; set; }

        public string ZipCode { get; set; }

        public string Phone { get; set; }

    }
}
