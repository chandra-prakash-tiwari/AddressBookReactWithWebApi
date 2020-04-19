using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Address_Book_Web_Application
{
    public class Services:IServices
    {
        private Context Db { get; set; }

        public Services(Context context)
        {
            this.Db = context;
        }
        public bool AddNewContact(Models contact)
        {
            contact.Id = Guid.NewGuid().ToString();
            this.Db.Contacts.Add(contact);
            return this.Db.SaveChanges() >0;
        }

        public Models GetContactById(string id)
        {
            return this.Db.Contacts?.FirstOrDefault(a => a.Id == id);
        }

        public List<Models> GetAllContacts()
        {
            return this.Db.Contacts?.ToList();
        }

        public bool DeleteContact(string id)
        {
            this.Db.Contacts.Remove(this.Db.Contacts?.FirstOrDefault(a => a.Id == id));
            return this.Db.SaveChanges()>0;
        }

        public bool EditContact(Models contact,string id)
        {
            var oldDetails = this.Db.Contacts?.FirstOrDefault(a => a.Id == id);
            if (oldDetails != null)
            {
                oldDetails.Name=contact.Name;
                oldDetails.Mobile=contact.Mobile;
                oldDetails.Landline=contact.Landline;
                oldDetails.Address=contact.Address;
                oldDetails.Email=contact.Email; 
                return this.Db.SaveChanges() > 0;
            }
            return false;
        }
    }
}
