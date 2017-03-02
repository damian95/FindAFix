using FindAFixMVC.Models.DBContext;
using FindAFixMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAFixMVCTest.Models
{
    class FakeHomeRepository : HomeIRepository<Technician>
    {
        public Technician GetTechByUserId(string id)
        {
            return new Technician() { AspNetUser_Id = id};
        }

        public int GetTechIdByUserId(string id)
        {
            return 1;
        }

        public TechnicianProfile GetTechProfileByTechId(int id)
        {
            return new TechnicianProfile() { techId = id};
        }

        public string getUserId()
        {
            return "178gr2u3br";
        }

        public void PostTech(Technician tech)
        {
            
        }

        public void PostTechProfile(TechnicianProfile profile)
        {
            
        }
    }
}
