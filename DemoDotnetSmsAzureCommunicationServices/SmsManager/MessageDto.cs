namespace SmsManager
{
    public class MessageDto
    {
        public required string Message { get; set; }
        public required List<string> PhoneNumbers { get; set; }
        public required string Sender { get; set; }
        public string? Tag { get; set; }
        public MessageDto() => PhoneNumbers = [];
    }
}