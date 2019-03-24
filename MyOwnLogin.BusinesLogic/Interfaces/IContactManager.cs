using MyOwnLogin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnLogin.BusinesLogic.Interfaces
{
    public interface IContactManager
    {
        List<Contact> GetContacts();

        void CreateContact(Contact contact);

        void DeleteContact(Guid id);

        Contact Details(Guid id);

        Contact GetContact(Guid id);

        void Edit(Contact contact);

    }
}
