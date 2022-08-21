using Library.Models.Book.DBMBook;
using Library.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

public class BookController : Controller
{
    private DBMBook dBMBook = new();
    public IActionResult Index()
    {
        return View();
    }

    public ActionResult BookList()
    {
        DTOModel dto = new();

        dto.BookList = dBMBook.LoadBookList();

        return View(dto);
    }

    [HttpPost]
    public IActionResult BookList(DTOModel dto)
    {
        dto.ClientList = new();

        return View(dto);
    }
}

