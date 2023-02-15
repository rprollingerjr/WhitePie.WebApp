namespace WhitePie.Models.Settings
{
    public class MomentsDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string MomentsCollectionName { get; set; } = null!;
    }
}
