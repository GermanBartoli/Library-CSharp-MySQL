using Library.Models.Book;
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
        dto.BookList = new();

        return View(dto);
    }

        public ActionResult NewBook(int? Book_Id)
    {
        // List<MartialStatusModel> martialStatus = dBMMartialStatus.LoadComboBoxMartialStatus();

        // ViewBag.MartialStatus = martialStatus;

        DTOModel dto = new();

        if (Book_Id == 0 || Book_Id == null)
        {
            return View(dto);
        }
        else
        {
            dto = dBMBook.GetBookByBookID(Book_Id);
            if (dto == null)
            {
                return NotFound();
            }
            return View(dto);
        }
    }

    [HttpPost]
    public IActionResult NewBook(DTOModel dto)
    {
        if (dto.Book.Book_Id == 0)
        {
            bool successAddBook;

            successAddBook = dBMBook.InsertBook(dto.Book);
            dto.Book.Book_Id = dBMBook.GetLastBookID();

            if (successAddBook)
            {
                return RedirectToAction("ConsultBook", "Book", new { Book_Id = dto.Book.Book_Id });
            }
            else
            {
                return RedirectToAction("BookList", "Book");
            }
        }
        else
        {
            bool successEditBook;

            successEditBook = dBMBook.UpdateBook(dto.Book);

            if (successEditBook)
            {
                return RedirectToAction("ConsultBook", "Book", new { Book_Id = dto.Book.Book_Id });
            }
            else
            {
                return RedirectToAction("BookList", "Book");
            }
        }
    }

    public ActionResult DisableBook(int Book_Id)
    {
        dBMBook.DisableBook(Book_Id);

        return RedirectToAction("BookList", "Book");
    }

    [HttpPost]
    public IActionResult DisableBook(BookModel book)
    {
        return View("BookList");
    }

    public ActionResult ConsultBook(int? Book_Id)
    {
        // List<MartialStatusModel> martialStatus = dBMMartialStatus.LoadComboBoxMartialStatus();

        // ViewBag.MartialStatus = martialStatus;

        if (Book_Id == 0 || Book_Id == null)
        {
            DTOModel dto = new();

            return View(dto);
        }
        else
        {
            DTOModel dto = new();

            dto = dBMBook.GetBookByBookID(Book_Id);

            if (dto == null)
            {
                return NotFound();
            }
            return View(dto);
        }
    }

    [HttpPost]
    public IActionResult ConsultBook(DTOModel dto)
    {
        return View();
    }
}

