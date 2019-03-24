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

namespace PhoneBookApi.Controllers.V2
{
    [ApiVersion("2.0")]
    [RoutePrefix("api/v{version:apiVersion}/phonebook")]
    [ValidationFilter]
    [ApiExceptionFilter]
    [Authorize]
    public class PhoneBookController : ApiController
    {
        private readonly ContactRepository _repository;

        public PhoneBookController()
        {
            _repository = new ContactRepository();
        }

        [Route("contacts")]
        public IHttpActionResult GetContacts()
        {
            IEnumerable<Contact> contacts = _repository.GetAll();
            return contacts != null
                ? CreateResponse(HttpStatusCode.OK, contacts)
                : CreateErrorResponse(HttpStatusCode.NotFound, "No contacts found");
        }

        [Route("contacts/{id}")]
        public IHttpActionResult Get(Guid id)
        {
            Contact contact = _repository.Get(id);
            return contact != null
                ? CreateResponse(HttpStatusCode.OK, contact)
                : CreateErrorResponse(HttpStatusCode.NotFound, "No contacts found");
        }

        [Route("contacts/search/{name}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchContacts(string name)
        {
            IEnumerable<Contact> contacts = _repository.GetAll()
                .Where(c => c.FullName.ToLower().Contains(name.ToLower()));

            return contacts != null && contacts.Any()
                ? CreateResponse(HttpStatusCode.OK, contacts)
                : CreateErrorResponse(HttpStatusCode.NotFound, "No contacts found");
        }

        [Route("contacts")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Post([FromBody]Contact contact)
        {
            _repository.Create(contact);
            return CreateResponse(HttpStatusCode.Created);
        }

        private IHttpActionResult CreateResponse(HttpStatusCode statusCode)
        {
            HttpResponseMessage message = Request.CreateResponse(statusCode);
            return ResponseMessage(message);
        }

        private IHttpActionResult CreateResponse<T>(HttpStatusCode statusCode, T value)
        {
            HttpResponseMessage message = Request.CreateResponse<T>(statusCode, value);
            return ResponseMessage(message);
        }

        private IHttpActionResult CreateErrorResponse(HttpStatusCode statusCode, string error)
        {
            HttpResponseMessage message = Request.CreateErrorResponse(statusCode, error);
            return ResponseMessage(message);
        }
    }
}