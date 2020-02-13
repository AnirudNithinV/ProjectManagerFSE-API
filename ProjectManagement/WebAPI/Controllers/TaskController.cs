using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/Task")]
    public class TaskController : ApiController
    {
        public IRepository<Task> taskRepository = null;
        public IRepository<Project> projectRepository = null;
        public IRepository<Parent_Task> parentRepository = null;
        public IRepository<User> userRepository = null;
        public TaskController()
        {
            this.taskRepository = new Repository<Task>();
            this.projectRepository = new Repository<Project>();
            this.parentRepository = new Repository<Parent_Task>();
            this.userRepository = new Repository<User>();
        }

        [Route("Delete/{id}")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult DeleteTask(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Task id");

           
            this.taskRepository.Delete(id);
            return Ok();
        }

       
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("GetTasks")]
        public IHttpActionResult GetTasks()
        {
            var parentTasks = parentRepository.GetAll();
            var projects = projectRepository.GetAll();

            var taskViewModels = taskRepository.GetAll().Select(x => new TaskModel
            {
                TaskID = x.TaskID,
                TaskName = x.Task1,
                ParentTaskID = x.ParentID,
                ParentTaskName = parentTasks.FirstOrDefault(y => y.ParentID == x.ParentID)?.ParentTask,
                Priority = x.Priority,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = x.Status,
                ProjectID = x.ProjectID,
                ProjectName = projects.FirstOrDefault(y => y.ProjectID == x.ProjectID)?.ProjectTitle
            }).ToList();
            return Ok(taskViewModels);
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreateTask(TaskModel taskModel)
        {
            if (taskModel != null)
            {
                if(taskModel.IsParentTask)
                {
                    var parentTask = new Parent_Task
                    {
                        ParentTask = taskModel.TaskName
                    };

                    parentRepository.Insert(parentTask);
                 
                }
                else
                {
                    var task = new Task
                    {
                        Task1 = taskModel.TaskName,
                        ProjectID = taskModel.ProjectID,
                        ParentID = taskModel.ParentTaskID,
                        Priority = taskModel.Priority,
                        StartDate = taskModel.StartDate,
                        EndDate = taskModel.EndDate,
                        Status = taskModel.Status
                    };
                    var taskAdded = taskRepository.Insert(task);

                    if (taskModel.UserID.HasValue)
                    {

                        var user = userRepository.GetAll().FirstOrDefault(x => x.UserID == taskModel.UserID);
                        user.TaskID = taskAdded.TaskID;
                        user.ProjectID = taskModel.ProjectID;

                        userRepository.Insert(user);
                    }
                }
            }
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult UpdateTask(TaskModel taskModel)
        {
            var task = new Task
            {
                TaskID = taskModel.TaskID,
                ParentID = taskModel.ParentTaskID,
                ProjectID = taskModel.ProjectID,
                Task1 = taskModel.TaskName,
                StartDate = taskModel.StartDate,
                EndDate = taskModel.EndDate,
                Priority = taskModel.Priority,
                Status = taskModel.Status
              
            };
            
            taskRepository.Update(task);
            if (taskModel.UserID.HasValue)
            {

                var user = userRepository.GetAll().FirstOrDefault(x => x.UserID == taskModel.UserID);
                user.TaskID = taskModel.TaskID;
                user.ProjectID = taskModel.ProjectID;

                userRepository.Update(user);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}