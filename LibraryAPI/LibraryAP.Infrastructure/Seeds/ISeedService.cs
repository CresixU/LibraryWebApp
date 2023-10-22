namespace LibraryAPI.Infrastructure.Seeds
{
    public interface ISeedService
    {
        public Task<bool> ExecuteSeeds();
    }
}
