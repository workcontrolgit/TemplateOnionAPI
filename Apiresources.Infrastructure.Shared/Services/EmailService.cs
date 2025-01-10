namespace $safeprojectname$.Services
{
    // Service for sending emails
    public class EmailService : IEmailService
    {
        // Mail settings injected from configuration
        public MailSettings _mailSettings { get; }

        // Logger for logging error messages or information
        public ILogger<EmailService> _logger { get; }

        // Constructor that accepts mail settings and a logger
        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {
            // Assign mail settings
            _mailSettings = mailSettings.Value;
            // Assign logger
            _logger = logger;
        }

        // Asynchronous method to send an email
        public async Task SendAsync(EmailRequest request)
        {
            try
            {
                // Create a new email message
                var email = new MimeMessage();
                // Set the sender email address, use default if not provided
                email.Sender = MailboxAddress.Parse(request.From ?? _mailSettings.EmailFrom);
                // Add recipient email address
                email.To.Add(MailboxAddress.Parse(request.To));
                // Assign subject to the email
                email.Subject = request.Subject;
                
                // Create a new body builder for the email body
                var builder = new BodyBuilder();
                // Set the HTML body of the email
                builder.HtmlBody = request.Body;
                // Assign the body to the email message
                email.Body = builder.ToMessageBody();

                // Initialize an SMTP client
                using var smtp = new SmtpClient();
                // Connect to the SMTP server with StartTls
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                // Authenticate with the SMTP server
                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                
                // Send the email asynchronously
                await smtp.SendAsync(email);
                // Disconnect from the SMTP server
                smtp.Disconnect(true);
            }
            catch (System.Exception ex)
            {
                // Log the error if sending fails
                _logger.LogError(ex.Message, ex);
                // Throw a custom API exception
                throw new ApiException(ex.Message);
            }
        }
    }
}