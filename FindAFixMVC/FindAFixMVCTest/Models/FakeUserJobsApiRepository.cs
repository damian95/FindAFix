using FindAFixMVC.Models.DBContext;
using FindAFixMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAFixMVCTest.Models
{
    class FakeUserJobsApiRepository : ApiIRepository<UserJob>
    {
        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int? id)
        {
            throw new NotImplementedException();
        }

        public ICollection<UserJob> Get()
        {
            throw new NotImplementedException();
        }

        public UserJob Get(int? id)
        {
            return new UserJob() { Id = (int)id };
        }

        public void Post(UserJob model)
        {
            throw new NotImplementedException();
        }

        public void Put(UserJob model)
        {
            throw new NotImplementedException();
        }
    }
}
