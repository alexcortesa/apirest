using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data
{
    public class Contact
    {
        private string firstName = string.Empty;
        private string lastName = string.Empty;
        private string phoneNumber = string.Empty;
        private string movil = string.Empty;
        private string email = string.Empty;
        private DateTime? dateCreate = null;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string PhoneMovil
        {
            get { return movil; }
            set { movil = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public DateTime? DateCreate
        {
            get { return dateCreate; }
            set { dateCreate = value; }
        }
    }
}