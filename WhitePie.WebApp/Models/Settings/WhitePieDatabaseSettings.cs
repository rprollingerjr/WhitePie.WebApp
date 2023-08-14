namespace WhitePie.Models.Settings
{
    public class WhitePieDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string MomentsCollectionName { get; set; } = null!;

        public string EventsCollectionName { get; set; } = null!;
    }
}
