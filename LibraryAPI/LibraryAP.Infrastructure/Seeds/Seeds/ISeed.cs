namespace LibraryAPI.Infrastructure.Seeds.Seeds
{
    public interface ISeed
    {
        public Task<bool> SeedData();
    }
}
