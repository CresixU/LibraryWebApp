using LibraryAPI.Data.Seeds;

namespace LibraryAPI.Data
{
    public sealed class SeedService : ISeedService
    {
        private readonly IEnumerable<ISeed> _seeds;
        public SeedService(IEnumerable<ISeed> seeds)
        {
            _seeds = seeds;

        }
        public async Task<bool> ExecuteSeeds()
        {
            foreach (var seed in _seeds)
            {
                await seed.SeedData();
            }
            return true;
        }
    }
}
