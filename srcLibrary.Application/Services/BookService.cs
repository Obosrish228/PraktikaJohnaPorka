public interface IBookService
{
    Task IssueBook(int bookId, int readerId, DateTime dueDate);
    Task ReturnBook(int bookId, int readerId);
    Task RenewBook(int bookId, int readerId, DateTime newDueDate);
}

public class BookService : IBookService
{
    private readonly LibraryDbContext _db;

    public BookService(LibraryDbContext db) => _db = db;

    public async Task IssueBook(int bookId, int readerId, DateTime dueDate)
    {
        var book = await _db.Books.FindAsync(bookId);
        if (book == null || !book.IsAvailable) throw new Exception("Book not available");

        book.IsAvailable = false;
        _db.BookIssuanceEvents.Add(new BookIssuanceEvent
        {
            BookId = bookId,
            ReaderId = readerId,
            DueDate = dueDate
        });
        await _db.SaveChangesAsync();
    }

    public async Task ReturnBook(int bookId, int readerId)
    {
        var book = await _db.Books.FindAsync(bookId);
        if (book == null) throw new Exception("Book not found");

        var issuance = await _db.BookIssuanceEvents
            .FirstOrDefaultAsync(e => e.BookId == bookId && e.ReaderId == readerId);

        book.IsAvailable = true;
        _db.BookReturnEvents.Add(new BookReturnEvent
        {
            BookId = bookId,
            ReaderId = readerId,
            IsLate = issuance != null && issuance.DueDate < DateTime.UtcNow
        });
        await _db.SaveChangesAsync();
    }

    public async Task RenewBook(int bookId, int readerId, DateTime newDueDate)
    {
        var reader = await _db.Readers.FindAsync(readerId);
        if (reader != null) reader.RenewalCount++;

        _db.BookRenewalEvents.Add(new BookRenewalEvent
        {
            BookId = bookId,
            ReaderId = readerId,
            NewDueDate = newDueDate
        });
        await _db.SaveChangesAsync();
    }
}
