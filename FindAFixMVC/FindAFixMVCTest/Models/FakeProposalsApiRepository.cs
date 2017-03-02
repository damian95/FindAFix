using FindAFixMVC.Models.DBContext;
using FindAFixMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAFixMVCTest.Models
{
    class FakeProposalsApiRepository : ApiIRepository<Proposal>
    {
        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int? id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Proposal> Get()
        {
            throw new NotImplementedException();
        }

        public Proposal Get(int? id)
        {
            return new Proposal() { Id = (int)id };
        }

        public void Post(Proposal model)
        {
            throw new NotImplementedException();
        }

        public void Put(Proposal model)
        {
            throw new NotImplementedException();
        }
    }
}
