using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Phonebook.Data;
using System.Web.Http.Cors;

namespace Phonebook.API.Controllers
{
    [AllowAnonymous]
    public class ContactsController : ApiController
    {
        [HttpGet]
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
        public HttpResponseMessage GetContacts()
        {
            List<GetContactsResult> listContacts = new ContactDataAccess().SelectContacts();
            return Request.CreateResponse<List<GetContactsResult>>(HttpStatusCode.OK, listContacts);
        }

        [HttpPost]
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
        public HttpResponseMessage Post([FromBody] Contact contact)
        {
            bool valid = false;
            new ContactDataAccess().InsertContact(contact, out valid);

            return Request.CreateResponse<bool>(HttpStatusCode.OK, valid);
        }

        [HttpDelete]
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
        public HttpResponseMessage Delete(string id)
        {
            new ContactDataAccess().DeleteContact(Guid.Parse(id));
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
