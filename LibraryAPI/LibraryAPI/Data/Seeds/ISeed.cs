namespace LibraryAPI.Data.Seeds
{
    public interface ISeed
    {
        public Task<bool> SeedData();
    }
}
