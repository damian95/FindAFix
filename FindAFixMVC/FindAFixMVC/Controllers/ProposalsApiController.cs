using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FindAFixMVC.Models;
using FindAFixMVC.Models.DBContext;
using FindAFixMVC.Models.Repositories;

namespace FindAFixMVC.Controllers
{
    public class ProposalsApiController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private ApiIRepository<Proposal> repository;
        //private ProposalApiRepository proposalApiRepository;

        public ProposalsApiController()
            : this(new ProposalApiRepository())
        { }

        public ProposalsApiController(ApiIRepository<Proposal> propRepository)
        {
            this.repository = propRepository;
        }

        /// <summary>
        /// Get Proposals
        /// </summary>
        /// <returns></returns>
        // GET: api/ProposalsApi
        public ICollection<Proposal> GetProposals()
        {
            return repository.Get();
        }

        /// <summary>
        /// Get Proposals
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/ProposalsApi/5
        [ResponseType(typeof(Proposal))]
        public IHttpActionResult GetProposal(int id)
        {
            Proposal proposal = repository.Get(id);
            if (proposal == null)
            {
                return NotFound();
            }

            return Ok(proposal);
        }

        /// <summary>
        /// Put Proposals
        /// </summary>
        /// <param name="id"></param>
        /// <param name="proposal"></param>
        /// <returns></returns>
        // PUT: api/ProposalsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProposal(int id, Proposal proposal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proposal.Id)
            {
                return BadRequest();
            }

            try
            {
                repository.Put(proposal);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProposalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Post Proposals
        /// </summary>
        /// <param name="proposal"></param>
        /// <returns></returns>
        // POST: api/ProposalsApi
        [ResponseType(typeof(Proposal))]
        public IHttpActionResult PostProposal(Proposal proposal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Post(proposal);

            return CreatedAtRoute("DefaultApi", new { id = proposal.Id }, proposal);
        }

        /// <summary>
        /// Delete Proposals
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ProposalsApi/5
        [ResponseType(typeof(Proposal))]
        public IHttpActionResult DeleteProposal(int id)
        {
            Proposal proposal = repository.Get(id);
            if (proposal == null)
            {
                return NotFound();
            }

            repository.Delete(id);

            return Ok(proposal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProposalExists(int id)
        {
            return repository.Exists(id);
        }
    }
}