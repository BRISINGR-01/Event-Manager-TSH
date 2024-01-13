namespace Logic.Configuration
{
    public class EmailConfig
    {
        public static string Section = "email";
        public string From { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
