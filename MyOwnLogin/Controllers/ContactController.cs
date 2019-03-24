using System.Web.Mvc;
using System;
//using MyOwnLogin.BusinesLogic.Managers;
using MyOwnLogin.Core.Models;
using MyOwnLogin.BusinesLogic.Interfaces;


namespace MyOwnLogin.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        IContactManager _repo;

        public ContactController(IContactManager repo)
        {
            _repo = repo;
        }

        public ActionResult GetContacts()
        {
            var contacts = _repo.GetContacts();

            return View(contacts);
        }

        [HttpGet]
        public ActionResult CreateContact()
        {
            Contact contact = new Contact();

            return View(contact);
        }

        [HttpPost]
        public ActionResult CreateContact(Contact contact)
        {
            _repo.CreateContact(contact);

            return RedirectToAction("GetContacts", "Contact");
        }
        
        public ActionResult Delete(Guid id)
        {
            _repo.DeleteContact(id);

            return RedirectToAction("GetContacts", "Contact");
        }

        public ActionResult Details(Guid id)
        {
            Contact contact = _repo.Details(id);

            return View(contact);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            Contact contact = _repo.GetContact(id);

            return View(contact);
        }

        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            _repo.Edit(contact);

            return RedirectToAction("GetContacts");
        }
    }
}