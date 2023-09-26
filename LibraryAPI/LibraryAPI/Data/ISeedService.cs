namespace LibraryAPI.Data
{
    public interface ISeedService
    {
        public Task<bool> ExecuteSeeds();
    }
}
