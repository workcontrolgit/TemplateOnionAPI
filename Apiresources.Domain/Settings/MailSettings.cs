namespace $safeprojectname$.Settings
{
    // Represents mail settings for an application.
    public class MailSettings
    {
        // Email address to send mail from.
        public string EmailFrom { get; set; }
        
        // SMTP server host name or IP address.
        public string SmtpHost { get; set; }
        
        // SMTP server port number.
        public int SmtpPort { get; set; }
        
        // Username for authentication with SMTP server.
        public string SmtpUser { get; set; }
        
        // Password for authentication with SMTP server.
        public string SmtpPass { get; set; }
        
        // Display name to appear in email headers.
        public string DisplayName { get; set; }
    }
}