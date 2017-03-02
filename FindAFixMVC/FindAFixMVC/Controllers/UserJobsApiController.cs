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
    public class UserJobsApiController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private ApiIRepository<UserJob> repository;
        //private UserJobsApiRepository userJobsApiRepository;

        public UserJobsApiController()
            : this(new UserJobsApiRepository())
        {
            //db.Configuration.LazyLoadingEnabled = false;
            //db.Configuration.ProxyCreationEnabled = false;
        }

        public UserJobsApiController(ApiIRepository<UserJob> ujRepository)
        {
            this.repository = ujRepository;
        }

        /// <summary>
        /// Get User Jobs
        /// </summary>
        /// <returns></returns>
        // GET: api/UserJobsApi
        public ICollection<UserJob> GetUserJobs()
        {
            return repository.Get();
        }

        /// <summary>
        /// Get User Jobs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/UserJobsApi/5
        [ResponseType(typeof(UserJob))]
        public IHttpActionResult GetUserJob(int id)
        {
            UserJob userjob = repository.Get(id);
            if (userjob == null)
            {
                return NotFound();
            }

            return Ok(userjob);
        }

        /// <summary>
        /// Put User Jobs
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userJob"></param>
        /// <returns></returns>
        // PUT: api/UserJobsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserJob(int id, UserJob userJob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userJob.Id)
            {
                return BadRequest();
            }

            try
            {
                repository.Put(userJob);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserJobExists(id))
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
        /// Post User Jobs
        /// </summary>
        /// <param name="userJob"></param>
        /// <returns></returns>
        // POST: api/UserJobsApi
        [ResponseType(typeof(UserJob))]
        public IHttpActionResult PostUserJob(UserJob userJob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Post(userJob);

            return CreatedAtRoute("DefaultApi", new { id = userJob.Id }, userJob);
        }

        /// <summary>
        /// Delete User Jobs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/UserJobsApi/5
        [ResponseType(typeof(UserJob))]
        public IHttpActionResult DeleteUserJob(int id)
        {
            UserJob userjob = repository.Get(id);
            if (userjob == null)
            {
                return NotFound();
            }
            repository.Delete(id);

            return Ok(userjob);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserJobExists(int id)
        {
            return repository.Exists(id);
        }
    }
}