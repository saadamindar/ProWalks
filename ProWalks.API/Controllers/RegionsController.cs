using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProWalks.API.CustomActionFilters;
using ProWalks.API.Data;
using ProWalks.API.Models.Domain;
using ProWalks.API.Models.DTO;
using ProWalks.API.Repositories;

namespace ProWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(AppDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            //get data from DB - domain model
            //var regions = await _dbContext.Regions.ToListAsync();
            var regions = await _regionRepository.GetAllAsync();

            //map domain models to DTOs
            //var regionDto = new List<RegionDto>();
            //foreach (var region in regions)
            //{
            //    regionDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        ImageUrl = region.ImageUrl,
            //        Country = region.Country
            //    });
            //}

            var regionDto = _mapper.Map<List<RegionDto>>(regions);

            //return DTO
            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById(Guid id) 
        {
            //var region = _dbContext.Regions.Find(id);

            var region = await _regionRepository.GetByIdAsync(id);

            if (region == null) {
                return NotFound();
            }


            var regionDto = _mapper.Map<RegionDto>(region);


            return Ok(regionDto);
        }

        [HttpPost]
        [ValidateModelAtribute]
        public async Task<IActionResult> Create([FromBody] AddRegionDto addRegionDto) 
        {
            var region = _mapper.Map<Region>(addRegionDto);

            region = await _regionRepository.CreateAsync(region);

            var regionDto = _mapper.Map<RegionDto>(region);

            return CreatedAtAction(nameof(GetRegionById), new {id = regionDto.Id}, regionDto);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModelAtribute]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto) 
        {
            //map dto to domain
            var region = _mapper.Map<Region>(updateRegionDto);

            region = await _regionRepository.UpdateAsync(id, region);

            //map domain to dto
            var regtionDto = _mapper.Map<RegionDto>(region);

            return Ok(regtionDto);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await _regionRepository.DeleteAsync(id);

            if (region == null) { 
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }
    }
}
