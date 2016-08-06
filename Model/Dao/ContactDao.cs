using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
    public class ContactDao
    {
        private BabyShopDbContext db = null;

        public ContactDao()
        {
            db = new BabyShopDbContext();
        }

        public Contact GetActiveContact()
        {
            return db.Contacts.Single(x => x.Status == true);
        }      

        public Contact ViewDetail(int id)
        {
            return db.Contacts.Find(id);
        }


        public int Insert(Contact entity)
        {
            try
            {
                db.Contacts.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public bool Update(Contact entity)
        {
            try
            {
                var Contact = db.Contacts.Find(entity.ID);
                Contact.Name = entity.Name;
                Contact.Phone = entity.Phone;
                Contact.Email = entity.Email;
                Contact.Website = entity.Website;
                Contact.Address = entity.Address;
                Contact.Lat = entity.Lat;
                Contact.Lng = entity.Lng;
                Contact.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public bool Delete(int id)
        {
            try
            {
                var Contact = db.Contacts.Find(id);
                db.Contacts.Remove(Contact);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Contact> ListAll()
        {
            return db.Contacts.ToList();
        }
    }
}