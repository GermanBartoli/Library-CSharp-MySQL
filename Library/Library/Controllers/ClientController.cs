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



        public ActionResult ConsultaCliente(int? idCliente)
        {
            List<MartialStatusModel> martialStatus = dBMMartialStatus.CargarComboGenero();

            ViewBag.Generos = generos;

            if (idCliente == 0 || idCliente == null)
            {
                DTOModel dto = new();

                return View(dto);
            }
            else
            {
                DTOModel dto = new();

                dto = gBDCliente.ObtenerClientexIdCliente(idCliente);

                if (dto == null)
                {
                    return NotFound();
                }
                return View(dto);
            }
        }

        [HttpPost]
        public IActionResult ConsultaCliente(DTOModel dto)
        {
            return View();
        }
    }
}
