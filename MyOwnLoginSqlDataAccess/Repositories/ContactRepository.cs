using MyOwnLogin.Core.Models;
using MyOwnLogin.DataAccess.Helpers;
using MyOwnLoginSqlDataAccess.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnLoginSqlDataAccess.Repositories
{
    public class ContactRepository :RestSharpHelper, IContactRepository
    {
        public List<Contact> GetContacts()
        {
            var contacts = Execute<List<Contact>>(RestServiceNames.GetContacts,
                                                                    Method.GET,
                                                                    null,
                                                                    true)
                                                                    .Data;

            return contacts;
        }

        public void CreateContact(Contact contact)
        {
            RestSharpHelper restSharpHelper = new RestSharpHelper();

            var result = restSharpHelper.Execute<Contact>(RestServiceNames.CreateContact,
                                                            Method.POST,
                                                            new Dictionary<string, object> { { "body", contact } },
                                                            true,
                                                            DataFormat.Json);
        }

        public void DeleteContact(Guid id)
        {
            RestSharpHelper restSharpHelper = new RestSharpHelper();

            restSharpHelper.Execute<Contact>(RestServiceNames.DeleteContact,
                Method.DELETE,
                new Dictionary<string, object> { { "id", id } },
                true
                );
        }

        public Contact Details(Guid id)
        {
            RestSharpHelper restSharpHelper = new RestSharpHelper();

            Contact contact = restSharpHelper.Execute<Contact>(
               RestServiceNames.GetContactById, Method.GET,
               new Dictionary<string, object> { { "id", id } }, true).Data;

            return contact;
        }

        public Contact GetContact(Guid id)
        {
            RestSharpHelper restSharpHelper = new RestSharpHelper();

            Contact contact = restSharpHelper.Execute<Contact>(
               RestServiceNames.GetContactById, Method.GET,
               new Dictionary<string, object> { { "id", id } }, true).Data;

            return contact;
        }

        public void Edit(Contact contact)
        {
            RestSharpHelper restSharpHelper = new RestSharpHelper();

            restSharpHelper.Execute<Contact>(RestServiceNames.UpdateContact,
                                                            Method.PUT,
                                                            new Dictionary<string, object> { { "body", contact } },
                                                            true,
                                                            DataFormat.Json);
        }
    }
}
