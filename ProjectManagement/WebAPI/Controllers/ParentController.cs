using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/Parent")]
    public class ParentController : ApiController
    {
        public IRepository<Parent_Task> parentRepository = null;
        public ParentController()
        {
            this.parentRepository = new Repository<Parent_Task>();
        }
       
        [Route("GetParentTasks")]
        // GET api/Employee
        public IHttpActionResult GetParentTasks()
        {
            var result = parentRepository.GetAll();
            return Ok(result);
        }       
    }
}