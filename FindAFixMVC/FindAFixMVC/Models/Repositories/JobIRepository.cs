using FindAFixMVC.Models.DBContext;
using FindAFixMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAFixMVC.Models.Repositories
{
    public interface JobIRepository<T>
    {
        string GetUserId();

        UserJob GetJob(int? id);

        List<UserJob> GetJobs();

        Proposal GetProposal(int id);

        Technician GetTech(int id);

        void PostProposal(Proposal proposal);

        void PostJob(UserJob job);

        void PutJob(UserJob job);

        void PutProposal(Proposal proposal);

        void DeleteJob(UserJob job);

        List<CategoryJobsViewModel> getTechsByCategory(String category);

        List<UserJob> getJobsByUserId(string id);

        List<UserJob> getJobsByTechId(int id);

        int getTechIdByUserId(string id);

        List<UserJob> getTechJobsByTechId(int id);

        List<Proposal> getProposalByJobId(int id);


    }
}
