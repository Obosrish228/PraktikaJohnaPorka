using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService) => _bookService = bookService;

    [HttpPost("{bookId}/issue")]
    public async Task<IActionResult> Issue(int bookId, [FromBody] IssueRequest request)
    {
        await _bookService.IssueBook(bookId, request.ReaderId, request.DueDate);
        return Ok();
    }

    [HttpPost("{bookId}/return")]
    public async Task<IActionResult> Return(int bookId, [FromBody] ReturnRequest request)
    {
        await _bookService.ReturnBook(bookId, request.ReaderId);
        return Ok();
    }

    [HttpPost("{bookId}/renew")]
    public async Task<IActionResult> Renew(int bookId, [FromBody] RenewRequest request)
    {
        await _bookService.RenewBook(bookId, request.ReaderId, request.NewDueDate);
        return Ok();
    }
}

public class IssueRequest { public int ReaderId { get; set; } public DateTime DueDate { get; set; } }
public class ReturnRequest { public int ReaderId { get; set; } }
public class RenewRequest { public int ReaderId { get; set; } public DateTime NewDueDate { get; set; } }
