namespace JwtAuthMicroservice.Models
{
    public class JwtSettings
    {
        public JwtSettings() { }

        public const string JwtSettingsName = "JwtSettings";
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DurationInMinutes { get; set; }

    }

}
