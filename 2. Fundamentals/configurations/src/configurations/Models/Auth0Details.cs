namespace configurations.Models
{
    public class Auth0Details
    {
        public string ClientId { get; set; }

        public int ClientSecret { get; set; }

        public int RetryCount { get; set; }
    }
}
