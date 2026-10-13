public interface IBookIssuanceEventRepository
{
    Task<List<BookIssuanceEvent>> GetAll();
    Task Add(BookIssuanceEvent issuanceEvent);
}
