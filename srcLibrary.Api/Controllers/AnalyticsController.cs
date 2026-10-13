using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;
    private readonly LibraryDbContext _db;

    public AnalyticsController(IAnalyticsService analyticsService, LibraryDbContext db)
    {
        _analyticsService = analyticsService;
        _db = db;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary() => Ok(await _analyticsService.GetSummary());

    [HttpGet("export")]
    public async Task<IActionResult> ExportEventsCsv()
    {
        var events = await _db.BookIssuanceEvents
            .Select(e => new { e.Id, e.BookId, e.ReaderId, e.IssueDate, e.DueDate })
            .ToListAsync();

        var sb = new StringBuilder();
        sb.AppendLine("Id,BookId,ReaderId,IssueDate,DueDate");
        foreach (var e in events)
            sb.AppendLine($"{e.Id},{e.BookId},{e.ReaderId},{e.IssueDate:yyyy-MM-dd},{e.DueDate:yyyy-MM-dd}");

        return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "events.csv");
    }
}
