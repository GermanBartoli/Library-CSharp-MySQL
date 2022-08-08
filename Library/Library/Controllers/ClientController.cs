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



        public ActionResult ConsultClient(int? Client_Id)
        {
            List<MartialStatusModel> martialStatus = dBMMartialStatus.LoadComboBoxMartialStatus();

            ViewBag.MartialStatus = martialStatus;

            if (Client_Id == 0 || Client_Id == null)
            {
                DTOModel dto = new();

                return View(dto);
            }
            else
            {
                DTOModel dto = new();

                dto = dBMClient.GetClientByCustomerID(Client_Id);

                if (dto == null)
                {
                    return NotFound();
                }
                return View(dto);
            }
        }

        [HttpPost]
        public IActionResult ConsultClient(DTOModel dto)
        {
            return View();
        }
    }
}
