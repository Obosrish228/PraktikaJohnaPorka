using Microsoft.EntityFrameworkCore;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Reader> Readers => Set<Reader>();
    public DbSet<BookIssuanceEvent> BookIssuanceEvents => Set<BookIssuanceEvent>();
    public DbSet<BookReturnEvent> BookReturnEvents => Set<BookReturnEvent>();
    public DbSet<BookRenewalEvent> BookRenewalEvents => Set<BookRenewalEvent>();
}
