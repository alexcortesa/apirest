using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Phonebook.API.Models;
using SICAI.Security;
using Phonebook.Data;
using System.Web.Http.Cors;

namespace Phonebook.API.Controllers
{
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        [HttpPost]
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
        public HttpResponseMessage Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            bool isCredentialValid = false;
            new LoginDataAccess().ValidateUser(login.Username, login.Password, out isCredentialValid); //true; //Valida a la base de datos;

            if (isCredentialValid)
            {
                var token = TokenManager.CreateJwtToken(login.Username);//TokenGenerator.GenerateTokenJwt(login.Username);
                return Request.CreateResponse<string>(HttpStatusCode.OK, token);
            }
            else
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}
