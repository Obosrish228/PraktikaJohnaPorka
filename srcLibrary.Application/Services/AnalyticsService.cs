public interface IAnalyticsService
{
    Task<AnalyticsSummaryResponse> GetSummary();
}

public class AnalyticsService : IAnalyticsService
{
    private readonly LibraryDbContext _db;

    public AnalyticsService(LibraryDbContext db) => _db = db;

    public async Task<AnalyticsSummaryResponse> GetSummary()
    {
        var totalBooks = await _db.Books.CountAsync();
        var activeReaders = await _db.Readers.CountAsync();
        var overdueBooks = await _db.BookIssuanceEvents
            .CountAsync(e => e.DueDate < DateTime.UtcNow
                && !_db.BookReturnEvents.Any(r => r.BookId == e.BookId));
        var avgRenewals = await _db.BookRenewalEvents
            .GroupBy(e => e.ReaderId)
            .Select(g => g.Count())
            .DefaultIfEmpty(0)
            .AverageAsync();

        return new AnalyticsSummaryResponse
        {
            TotalBooks = totalBooks,
            ActiveReaders = activeReaders,
            OverdueBooks = overdueBooks,
            ActiveReadersCount = activeReaders,
            AverageRenewalsPerReader = avgRenewals
        };
    }
}
