namespace MobileKoisk.Api.Models
{
    // Notification Model
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
    }

    public enum NotificationType
    {
        Transaction,
        PaymentConfirmation,
        SecurityAlert,
        Promotion
    }
}
