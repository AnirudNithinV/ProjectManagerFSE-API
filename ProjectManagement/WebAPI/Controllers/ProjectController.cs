using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/Project")]
    public class ProjectController : ApiController
    {
        public IRepository<Project> projectRepository = null;
        public IRepository<User> userRepository = null;
        public ProjectController()
        {
            this.projectRepository = new Repository<Project>();
            this.userRepository = new Repository<User>();
            
        }

        /// <summary>
        /// Action method to get all projects
        /// </summary>
        /// <returns>Returns action result</returns>
        // GET api/Employee
        [Route("GetProjects")]
        public IHttpActionResult GetProjects()
        {
            var result = projectRepository.GetAll();

            if (result.Count() == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }


        /// <summary>
        /// Action method to delete project by id
        /// </summary>
        /// <returns>Returns action result</returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult DeleteProjectById(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid User id");

            projectRepository.Delete(id);           

            return Ok();
        }

        /// <summary>
        /// Action method to create new project
        /// </summary>
        /// <returns>Returns action result</returns>
        // POST api/Employee
        [Route("Create")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateProject(ProjectDBModel project)
        {
            Project prj = new Project();
            prj.EndDate = project.EndDate;
            prj.StartDate = project.StartDate;
            prj.ProjectTitle = project.ProjectTitle;
            prj.Priority = project.Priority;           
            projectRepository.Insert(prj);

            int ProjectID = prj.ProjectID;
            if (project.UserID != null)
            {
                User UserData = userRepository.GetById(project.UserID);
                UserData.ProjectID = ProjectID;
                userRepository.Update(UserData);
            }
            return Ok();
        }

        /// <summary>
        /// Action method to update existing project
        /// </summary>
        /// <returns>Returns action result</returns>
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult UpdateProject(Project project)
        {
            projectRepository.Update(project);
           
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}