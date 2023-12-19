using MimeKit;

namespace debtTrackingApp.Services.Email
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(m => new MailboxAddress(address:m, name:"email")));
            Subject = subject;
            Content = content;
        }
    }
    
}
