using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ProWalks.API.Data;
using ProWalks.API.Models.Domain;

namespace ProWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly AppDbContext _dbContext;
        public RegionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var region = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null) 
            {
                return null;
            }

            _dbContext.Regions.Remove(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id  == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var exists = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (exists == null)
            {
                return null;
            }

            exists.Code = region.Code;
            exists.Name = region.Name;
            exists.ImageUrl = region.ImageUrl;
            exists.Country = region.Country;

            await _dbContext.SaveChangesAsync();
            return exists;
        }
    }
}
