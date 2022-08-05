using Library.Models;
using Library.Models.Client.DBMClient;
using Library.Models.Client;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Library.Models.Client.MartialStatus;
using Library.Models.DTO;

namespace Library.Controllers
{
    public class ClientController : Controller
    {
        private DBMClient dBMClient = new();
        private DBMMartialStatus dBMMartialStatus = new();

        public ActionResult ClientList()
        {
            DTOModel dto = new();

            dto.ClientList = dBMClient.LoadClientList();

            return View(dto);
        }
    }
}