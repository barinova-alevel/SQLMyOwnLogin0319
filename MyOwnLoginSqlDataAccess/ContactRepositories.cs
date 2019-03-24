using MyOwnLogin.BusinesLogic.Interfaces;
using MyOwnLogin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnLoginSqlDataAccess
{
    public class ContactRepositories : IContactRepository
    {
        public void CreateContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void DeleteContact(Guid id)
        {
            throw new NotImplementedException();
        }

        public Contact Details(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Contact GetContact(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Contact> GetContacts()
        {
            throw new NotImplementedException();
        }
    }
}
