using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data
{
    public class ContactDataAccess
    {
        private ContactsDataDataContext dataContext;

        public ContactDataAccess()
        {
            dataContext = new ContactsDataDataContext();
        }

        /// <summary>
        /// Inserta un nuevo registro en la tabla Contacts.
        /// </summary>
        /// <param name="newContact"></param>
        /// <param name="valid"></param>
        public void InsertContact(Contact newContact, out bool valid)
        {
            valid = dataContext.InsertContact(newContact.FirstName, newContact.LastName, newContact.Email,
                newContact.PhoneNumber, newContact.PhoneMovil).Equals(1) ? true : false;
        }

        /// <summary>
        /// Elimina un registro de la tabla Contacts.
        /// </summary>
        /// <param name="contactId"></param>
        public void DeleteContact(Guid contactId)
        {
            dataContext.DeleteContact(contactId);
        }

        /// <summary>
        /// Obtiene todos los registros de la tabla contacts, ordenados por 
        /// lastname, firstname.
        /// </summary>
        /// <returns></returns>
        public List<GetContactsResult> SelectContacts()
        {
            return dataContext.GetContacts().ToList();
        }
    }
}
