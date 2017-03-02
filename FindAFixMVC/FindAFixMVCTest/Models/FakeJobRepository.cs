using FindAFixMVC.Models.DBContext;
using FindAFixMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAFixMVC.Models.ViewModels;

namespace FindAFixMVCTest.Models
{
    class FakeJobRepository : JobIRepository<UserJob>
    {
        public string GetUserId()
        {
            return "9g7f86fghihhgg8g";
        }

        public void DeleteJob(UserJob job)
        {
            
        }

        public UserJob GetJob(int? id)
        {
            return new UserJob() { Id = (int)id };
        }

        public List<UserJob> GetJobs()
        {
            return new List<UserJob>();
        }

        public List<UserJob> getJobsByTechId(int id)
        {
            return new List<UserJob>();
        }

        public List<UserJob> getJobsByUserId(string id)
        {
            return new List<UserJob>();
        }

        public Proposal GetProposal(int id)
        {
            return new Proposal() { Id = (int)id};
        }

        public List<Proposal> getProposalByJobId(int id)
        {
            return new List<Proposal>();
        }

        public Technician GetTech(int id)
        {
            return new Technician() { Id = (int)id };
        }

        public int getTechIdByUserId(string id)
        {
            return 1;
        }

        public List<UserJob> getTechJobsByTechId(int id)
        {
            return new List<UserJob>();
        }

        public List<CategoryJobsViewModel> getTechsByCategory(string category)
        {
            return new List<CategoryJobsViewModel>();
        }

        public void PostJob(UserJob job)
        {
            
        }

        public void PostProposal(Proposal proposal)
        {
            
        }

        public void PutJob(UserJob job)
        {
           
        }

        public void PutProposal(Proposal proposal)
        {
            
        }
    }
}
