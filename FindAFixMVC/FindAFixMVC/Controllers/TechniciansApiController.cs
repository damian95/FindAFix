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
    public class TechniciansApiController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private ApiIRepository<Technician> repository;
        //private TechnicianApiRepository technicianApiRepository;

        public TechniciansApiController()
            : this(new TechnicianApiRepository())
        {

        }

        public TechniciansApiController(ApiIRepository<Technician> techRepository)
        {
            this.repository = techRepository;
        }


        /// <summary>
        /// Get Technicians
        /// </summary>
        /// <returns></returns>
        // GET: api/TechniciansApi
        public ICollection<Technician> GetTechnicians()
        {
            return repository.Get();
        }

        /// <summary>
        /// Get Technicians
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/TechniciansApi/5
        [ResponseType(typeof(Technician))]
        public IHttpActionResult GetTechnician(int id)
        {
            Technician technician = repository.Get(id);
            if (technician == null)
            {
                return NotFound();
            }

            return Ok(technician);
        }

        /// <summary>
        /// Put Technicians
        /// </summary>
        /// <param name="id"></param>
        /// <param name="technician"></param>
        /// <returns></returns>
        // PUT: api/TechniciansApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTechnician(int id, Technician technician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != technician.Id)
            {
                return BadRequest();
            }

            try
            {
                repository.Put(technician);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TechnicianExists(id))
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
        /// Post Technicians
        /// </summary>
        /// <param name="technician"></param>
        /// <returns></returns>
        // POST: api/TechniciansApi
        [ResponseType(typeof(Technician))]
        public IHttpActionResult PostTechnician(Technician technician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Post(technician);

            return CreatedAtRoute("DefaultApi", new { id = technician.Id }, technician);
        }

        /// <summary>
        /// Delete Technicians
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/TechniciansApi/5
        [ResponseType(typeof(Technician))]
        public IHttpActionResult DeleteTechnician(int id)
        {
            Technician technician = repository.Get(id);
            if (technician == null)
            {
                return NotFound();
            }
            repository.Delete(id);
            return Ok(technician);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TechnicianExists(int id)
        {
            return repository.Exists(id);
        }
    }
}