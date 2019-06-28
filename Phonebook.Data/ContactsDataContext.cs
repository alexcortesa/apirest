using System.Configuration;
using System.Data.Linq;
using System.Web;

namespace Phonebook.Data
{
    public partial class ContactsDataDataContext : DataContext
    {
        private static string session = typeof(ContactsDataDataContext).Name;

        public static ContactsDataDataContext GetDataContext()
        {
            if(HttpContext.Current.Session[session] == null)
            {
                var context = new ContactsDataDataContext();
                HttpContext.Current.Session.Add(session, context);
                return context;
            }

            return (ContactsDataDataContext)HttpContext.Current.Session[session];
        }

        public ContactsDataDataContext()
            :base(ConfigurationManager.ConnectionStrings["ContactsDB.ConnectionSting"].ConnectionString)
        {
            OnCreated();
        }
    }
}
