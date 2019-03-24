using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhoneBookApi.Models;
using PhoneBookApi.Attributes;
using PhoneBookApi.Repositories;
using Microsoft.Web.Http;

namespace PhoneBookApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/phonebook")]
    public class PhoneBookController : ApiController
    {
        [Route("contacts")]
        public List<Contact> GetContacts()
        {
            return new List<Contact>
            {
                new Contact
                {
                    FirstName = "Robin",
                    LastName = "Hood"
                }
            };
        }
    }
}
