namespace $safeprojectname$.DTOs.Email
{
    // Represents an email request with properties for recipient address, subject line, message body, and sender address
    public class EmailRequest
    {
        // Recipient address of the email
        public string To { get; set; }
        // Subject line of the email
        public string Subject { get; set; }
        // Message body of the email
        public string Body { get; set; }
        // Sender address of the email
        public string From { get; set; }
    }
}