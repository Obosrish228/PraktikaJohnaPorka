public class BookIssuanceEventRepository : IBookIssuanceEventRepository
{
    private readonly LibraryDbContext _db;

    public BookIssuanceEventRepository(LibraryDbContext db) => _db = db;

    public async Task<List<BookIssuanceEvent>> GetAll() =>
        await _db.BookIssuanceEvents.ToListAsync();

    public async Task Add(BookIssuanceEvent issuanceEvent)
    {
        _db.BookIssuanceEvents.Add(issuanceEvent);
        await _db.SaveChangesAsync();
    }
}
