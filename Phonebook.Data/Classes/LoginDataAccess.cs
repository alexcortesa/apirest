using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data
{
    public class LoginDataAccess
    {
        private ContactsDataDataContext dataContext;

        public LoginDataAccess()
        {
            dataContext = new ContactsDataDataContext();
        }

        /// <summary>
        /// Verifica si el usuario existe en la DB.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public ValidaUserResult ValidateUser(string email, string pass, out bool valid)
        {
            ValidaUserResult result = dataContext.ValidaUser(email, pass).SingleOrDefault();

            valid = result != null;
            if (valid)
                return result;
            else
                return null;
        }
    }
}
