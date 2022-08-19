using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Library.Models.DTO;
using Library.Models.Author.DBMAuthor;
using Library.Models.Author;

namespace Library.Controllers
{
    public class AuthorController : Controller
    {
        private DBMAuthor dBMAuthor = new();

        public ActionResult AuthorList()
        {
            DTOModel dto = new();

            dto.AuthorList = dBMAuthor.LoadAuthorList();

            return View(dto);
        }

        [HttpPost]
        public IActionResult AuthorList(DTOModel dto)
        {
            dto.AuthorList = new();

            return View(dto);
        }

        public ActionResult NewAuthor(int? Author_Id)
        {
            DTOModel dto = new();

            if (Author_Id == 0 || Author_Id == null)
            {
                return View(dto);
            }
            else
            {
                dto = dBMAuthor.GetAuthorByAuthorID(Author_Id);
                if (dto == null)
                {
                    return NotFound();
                }
                return View(dto);
            }
        }

        [HttpPost]
        public IActionResult NewAuthor(DTOModel dto)
        {
            if (dto.Author.Author_Id == 0)
            {
                bool successAddAuthor;

                successAddAuthor = dBMAuthor.InsertAuthor(dto.Author);
                dto.Author.Author_Id = dBMAuthor.GetLastAuthorID();

                if (successAddAuthor)
                {
                    return RedirectToAction("ConsultAuthor", "Author", new { Author_Id = dto.Author.Author_Id });
                }
                else
                {
                    return RedirectToAction("AuthorList", "Author");
                }
            }
            else
            {
                bool successEditAuthor;

                successEditAuthor = dBMAuthor.UpdateAuthor(dto.Author);

                if (successEditAuthor)
                {
                    return RedirectToAction("ConsultAuthor", "Author", new { Author_Id = dto.Author.Author_Id });
                }
                else
                {
                    return RedirectToAction("AuthorList", "Author");
                }
            }
        }

        public ActionResult DisableAuthor(int Author_Id)
        {
            dBMAuthor.DisableAuthor(Author_Id);

            return RedirectToAction("AuthorList", "Author");
        }

        [HttpPost]
        public IActionResult DisableAuthor(AuthorModel author)
        {
            return View("AuthorList");
        }

        public ActionResult ConsultAuthor(int? Author_Id)
        {
            if (Author_Id == 0 || Author_Id == null)
            {
                DTOModel dto = new();

                return View(dto);
            }
            else
            {
                DTOModel dto = new();

                dto = dBMAuthor.GetAuthorByAuthorID(Author_Id);

                if (dto == null)
                {
                    return NotFound();
                }
                return View(dto);
            }
        }

        [HttpPost]
        public IActionResult ConsultAuthor(DTOModel dto)
        {
            return View();
        }
    }
}
