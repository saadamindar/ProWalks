using System.ComponentModel.DataAnnotations;

namespace ProWalks.API.Models.DTO
{
    public class CreateWalkDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double LengthInKm { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
