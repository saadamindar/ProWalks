using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProWalks.API.Models.Domain;
using ProWalks.API.Models.DTO;
using ProWalks.API.Repositories;

namespace ProWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWalkDto createWalkDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Map CreateWalksDto to Walks
            var walkDomain = mapper.Map<Walk>(createWalkDto);

            await walkRepository.CreateAsync(walkDomain);

            var walkDto = mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 3)
        {
            var walks = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            var walksDto = mapper.Map<List<WalkDto>>(walks);
            return Ok(walksDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]UpdateWalkDto updateWalkDto)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var walkDomain = mapper.Map<Walk>(updateWalkDto);
            walkDomain.Id = id;
            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);
            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walkDomain);
            return Ok(walkDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            // Soft delete
            walk.IsActive = false;
            await walkRepository.UpdateAsync(id, walk);
            return NoContent();
        }
    }
}
