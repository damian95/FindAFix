using FindAFixMVC.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FindAFixMVC.Models.Repositories
{
    public class TechnicianApiRepository : ApiIRepository<Technician>
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public TechnicianApiRepository()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        public void Delete(int? id)
        {
            Technician tech = db.Technicians.Find(id);
            db.Technicians.Remove(tech);
            db.SaveChanges();
        }

        public ICollection<Technician> Get()
        {
            return db.Technicians.ToList();
        }

        public Technician Get(int? id)
        {
            return db.Technicians.Find(id);
        }

        public void Post(Technician tech)
        {
            db.Technicians.Add(tech);
            db.SaveChanges();
        }

        public void Put(Technician tech)
        {
            db.Entry(tech).State = EntityState.Modified;
            db.SaveChanges();
        }

        bool ApiIRepository<Technician>.Exists(int? id)
        {
            return db.Technicians.Count(e => e.Id == id) > 0;
        }

        //private bool Exists(int? id)
        //{
        //    return db.Technicians.Count(e => e.Id == id) > 0;
        //}
    }
}