using Library.Models.DTO;
using Library.Models.Transaction.DBMTransaction;

using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

public class TransactionController : Controller
{
    private DBMTransaction dBMTransaction = new();

    public ActionResult TransactionList()
    {
        DTOModel dto = new();

        dto.TransactionList = dBMTransaction.LoadTransactionList();

        return View(dto);
    }

    [HttpPost]
    public IActionResult TransactionList(DTOModel dto)
    {
        dto.TransactionList = new();

        return View(dto);
    }
}

