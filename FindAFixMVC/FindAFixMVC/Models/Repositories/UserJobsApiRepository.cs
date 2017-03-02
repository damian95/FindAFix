using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindAFixMVC.Models.DBContext;
using System.Data.Entity;

namespace FindAFixMVC.Models.Repositories
{
    public class UserJobsApiRepository : ApiIRepository<UserJob>
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public UserJobsApiRepository()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        public void Delete(int? id)
        {
            UserJob uj = db.UserJobs.Find(id);
            db.UserJobs.Remove(uj);
            db.SaveChanges();
        }

        public ICollection<UserJob> Get()
        {
            return db.UserJobs.ToList();
        }

        public UserJob Get(int? id)
        {
            return db.UserJobs.Find(id);
        }

        public void Post(UserJob uj)
        {
            db.UserJobs.Add(uj);
            db.SaveChanges();
        }

        public void Put(UserJob uj)
        {
            db.Entry(uj).State = EntityState.Modified;
            db.SaveChanges();
        }

        bool ApiIRepository<UserJob>.Exists(int? id)
        {
            return db.UserJobs.Count(e => e.Id == id) > 0;
        }

        //private bool Exists(int? id)
        //{
        //    return db.UserJobs.Count(e => e.Id == id) > 0;
        //}
    }
}