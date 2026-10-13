public class BookRenewalEvent
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int ReaderId { get; set; }
    public DateTime RenewalDate { get; set; } = DateTime.UtcNow;
    public DateTime NewDueDate { get; set; }
}
