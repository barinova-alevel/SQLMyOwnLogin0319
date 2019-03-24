using MyOwnLogin.BusinesLogic.Interfaces;
using MyOwnLogin.Core.Models;
using MyOwnLogin.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnLogin.BusinesLogic.Managers
{
    public class ContactManager : IContactManager
    {
        public readonly IContactRepository _repo;

        public ContactManager(IContactRepository repo)
        {
            _repo = repo;
        }

        public List<Contact> GetContacts()
        {
            return _repo.GetContacts();
        }
        
        public void CreateContact(Contact contact)
        {
            _repo.CreateContact(contact);
        }

        public void DeleteContact(Guid id)
        {
            _repo.DeleteContact(id);
        }

        public Contact Details(Guid id)
        {
            return _repo.Details(id);
        }
        
        public Contact GetContact(Guid id)
        {
            return _repo.GetContact(id);
        }
        
        public void Edit(Contact contact)
        {
            _repo.Edit(contact);
        }
    }
}
