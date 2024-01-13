namespace Logic.Configuration
{
    public class VapidConfig
    {
        public static string Section = "VAPID";
        public string Subject { get; set; } = string.Empty;
        public string PublicKey { get; set; } = string.Empty;
        public string PrivateKey { get; set; } = string.Empty;
    }
}
