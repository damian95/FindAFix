using FindAFixMVC.Models.DBContext;
using FindAFixMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAFixMVCTest.Models
{
    class FakeTechnicianApiRepository : ApiIRepository<Technician>
    {
        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int? id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Technician> Get()
        {
            throw new NotImplementedException();
        }

        public Technician Get(int? id)
        {
            return new Technician() { Id = (int)id };
        }

        public void Post(Technician model)
        {
            throw new NotImplementedException();
        }

        public void Put(Technician model)
        {
            throw new NotImplementedException();
        }
    }
}
