using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProWalks.API.Data;
using ProWalks.API.Models.Domain;
using ProWalks.API.Models.DTO;

namespace ProWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public RegionsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            //get data from DB - domain model
            var regions = _dbContext.Regions.ToList();

            //map domain models to DTOs
            var regionDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    ImageUrl = region.ImageUrl,
                    Country = region.Country
                });
            }

            //return DTO
            return Ok(regionDto);


            return Ok(regions);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRegionById(Guid id) 
        {
            //var region = _dbContext.Regions.Find(id);

            var region = _dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if (region == null) {
                return NotFound();
            }

            //map
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                ImageUrl = region.ImageUrl,
                Country = region.Country
            };


            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionDto addRegionDto) {
            //map dto and domain model
            var region = new Region
            {
                Code = addRegionDto.Code,
                Name = addRegionDto.Name,
                ImageUrl = addRegionDto.ImageUrl,
                Country = addRegionDto.Country
            };

            //use domain model to create record
            _dbContext.Regions.Add(region);
            _dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                ImageUrl = region.ImageUrl,
                Country = region.Country
            };

            return CreatedAtAction(nameof(GetRegionById), new {id = regionDto.Id}, regionDto);
        }
    }
}
