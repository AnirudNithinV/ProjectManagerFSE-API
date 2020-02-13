using System.Linq;
using System.Net;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        public IRepository<User> userRepository = null;
        public UserController()
        {
            this.userRepository = new Repository<User>();

        }
        // GET api/Employee
        [Route("GetUsers")]
        public IHttpActionResult GetUsers()
        {
            var result = userRepository.GetAll().Select(x => new UserModel
            {
                EmployeeID = x.EmployeeID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserID = x.UserID
            }).ToList();

            return Ok(result);
        }

        [Route("Delete/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid User id");

            userRepository.Delete(id);

            return Ok();
        }

        // POST api/Employee
        [Route("Create")]
        [HttpPost]
        public IHttpActionResult CreateUser(User user)
        {
            userRepository.Insert(user);
            return Ok();
        }

        [Route("Update")]
        [HttpPut]
        public IHttpActionResult UpdateUser(User user)
        {
            userRepository.Update(user);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}