using ProWalks.API.Models.Domain;

namespace ProWalks.API.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? ImageUrl { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = false;
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //Navigation
        public DifficultyDto Difficulty { get; set; }
        public RegionDto Region { get; set; }
    }
}
