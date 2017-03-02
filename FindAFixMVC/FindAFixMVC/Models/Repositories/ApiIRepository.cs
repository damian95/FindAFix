using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAFixMVC.Models.Repositories
{
    public interface ApiIRepository<T>
    {
        ICollection<T> Get();
        T Get(int? id);
        void Post(T model);
        void Put(T model);
        void Delete(int? id);
        bool Exists(int? id);
    }
    
}
