public class BookReturnEvent
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int ReaderId { get; set; }
    public DateTime ReturnDate { get; set; } = DateTime.UtcNow;
    public bool IsLate { get; set; }
}
