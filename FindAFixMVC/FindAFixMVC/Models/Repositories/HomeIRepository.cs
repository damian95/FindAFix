using FindAFixMVC.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAFixMVC.Models.Repositories
{
    public interface HomeIRepository<T>
    {
        string getUserId();

        Technician GetTechByUserId(string id);

        void PostTech(Technician tech);

        void PostTechProfile(TechnicianProfile profile);

        int GetTechIdByUserId(string id);

        TechnicianProfile GetTechProfileByTechId(int id);

    }
}
