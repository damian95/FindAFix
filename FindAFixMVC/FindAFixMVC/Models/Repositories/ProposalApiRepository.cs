using FindAFixMVC.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FindAFixMVC.Models.Repositories
{
    public class ProposalApiRepository : ApiIRepository<Proposal>
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ProposalApiRepository()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        public void Delete(int? id)
        {
            Proposal prop = db.Proposals.Find(id);
            db.Proposals.Remove(prop);
            db.SaveChanges();
        }

        public ICollection<Proposal> Get()
        {
            return db.Proposals.ToList();
        }

        public Proposal Get(int? id)
        {
            return db.Proposals.Find(id);
        }

        public void Post(Proposal prop)
        {
            db.Proposals.Add(prop);
            db.SaveChanges();
        }

        public void Put(Proposal prop)
        {
            db.Entry(prop).State = EntityState.Modified;
            db.SaveChanges();
        }

        bool ApiIRepository<Proposal>.Exists(int? id)
        {
            return db.Proposals.Count(e => e.Id == id) > 0;
        }

        //private bool Exists(int? id)
        //{
        //    return db.Proposals.Count(e => e.Id == id) > 0;
        //}
    }
}