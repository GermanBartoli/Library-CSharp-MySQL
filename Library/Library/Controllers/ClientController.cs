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

        [HttpPost]
        public IActionResult ClientList(DTOModel dto)
        {
            dto.ClientList = new();

            return View(dto);
        }

        public ActionResult NewClient(int? Client_Id)
        {
            List<MartialStatusModel> martialStatus = dBMMartialStatus.LoadComboBoxMartialStatus();

            ViewBag.MartialStatus = martialStatus;

            DTOModel dto = new();

            if (Client_Id == 0 || Client_Id == null)
            {
                return View(dto);
            }
            else
            {
                dto = dBMClient.GetClientByClientID(Client_Id);
                if (dto == null)
                {
                    return NotFound();
                }
                return View(dto);
            }
        }

        [HttpPost]
        public IActionResult NewClient(DTOModel dto)
        {
            if (dto.Client.Client_Id == 0)
            {
                bool successAddClient;

                successAddClient = dBMClient.AddClient(dto.Client);
                dto.Client.Client_Id = dBMClient.GetLastClientByID();

                if (successAddClient)
                {
                    return RedirectToAction("ConsultClient", "Client", new { Client_Id = dto.Client.Client_Id });
                }
                else
                {
                    return RedirectToAction("ClientList", "Client");
                }
            }
            else
            {
                bool successEditClient;

                successEditClient = dBMClient.EditClient(dto.Client);

                if (successEditClient)
                {
                    return RedirectToAction("ConsultClient", "Client", new { Client_Id = dto.Client.Client_Id });
                }
                else
                {
                    return RedirectToAction("ClientList", "Client");
                }
            }
        }

        public ActionResult DisableClient(int Client_Id)
        {
            dBMClient.DisableClient(Client_Id);

            return RedirectToAction("ClientList", "Client");
        }

        [HttpPost]
        public IActionResult DisableClient(ClientModel client)
        {
            return View("ClientList");
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

                dto = dBMClient.GetClientByClientID(Client_Id);

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
