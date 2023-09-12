namespace JwtAuthMicroservice.Models
{
    public class MongoSettings
    {
        public MongoSettings() { }

        public const string MongoSettingsName = "MongoSettings";
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
