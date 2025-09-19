using System.ComponentModel.DataAnnotations;

namespace ProWalks.API.Models.DTO
{
    public class AddRegionDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string Country { get; set; }
    }
}
