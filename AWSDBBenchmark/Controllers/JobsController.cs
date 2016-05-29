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
using AWSDBBenchmark.Models;
using AWSDBBenchmark.Models.Entities;

namespace AWSDBBenchmark.Controllers
{
    [RoutePrefix("api/jobs")]
    public class JobsController : ApiController
    {
        private Random random = new Random();
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("random")]
        // GET: api/Jobs
        public IQueryable<Job> GetJobsRandom()
        {
            var max = db.Jobs.Max(j => j.Id);
            var randomId = random.Next(max);
            return db.Jobs.Where(j => j.Id > randomId).Take(5);
        }

        [Route("total")]
        [HttpGet]
        // GET: api/Jobs
        public HttpResponseMessage GetTotal()
        {
            var max = db.Jobs.Count();
            return Request.CreateResponse(HttpStatusCode.OK, max);
        }

        [Route("")]
        // GET: api/Jobs
        public IQueryable<Job> GetJobs()
        {
            return db.Jobs;
        }
        [Route("")]
        [HttpPost]
        // POST: api/Jobs
        [ResponseType(typeof(Job))]
        public HttpResponseMessage PostJob(Job job)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Jobs.Add(job);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, job.Id);
        }

        // DELETE: api/Jobs/5
        [ResponseType(typeof(Job))]
        public IHttpActionResult DeleteJob(int id)
        {
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }

            db.Jobs.Remove(job);
            db.SaveChanges();

            return Ok(job);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}