using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using WebAPI.Models;
using System.Web;
using Moq;
using WebAPI.Repository;
using WebAPI;
using System.Collections.Generic;

namespace ProjectMgmt.UnitTest.Harness
{
    [TestClass]
    public class TaskControllerTest
    {
        Mock<IRepository<Parent_Task>> mockParentRepository = new Mock<IRepository<Parent_Task>>();
        Mock<IRepository<User>> mockUserRepository = new Mock<IRepository<User>>();
        Mock<IRepository<Project>> mockProjectRepository = new Mock<IRepository<Project>>();
        Mock<IRepository<Task>> mockTaskRepository = new Mock<IRepository<Task>>();

        TaskController controller;

        [TestInitialize]
        public void InitializeTest()
        {
            
            List<Task> taskList = new List<Task>();
            taskList.Add(new Task
            {
                TaskID = 1,
                Task1 = "Test Task",
                Priority = 1,
                ProjectID = 1,
                ParentID = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            });


            List<Parent_Task> parentTaskList = new List<Parent_Task>();
            parentTaskList.Add(new Parent_Task
            {
                ParentID = 1,
                ParentTask = "Parent Task1"
            });

            List<Project> projectList = new List<Project>();
            projectList.Add(new Project
            {
                ProjectID = 1,
                Priority = 1,
                ProjectTitle = "Test Project Title"
            });

            List<User> userList = new List<User>();
            userList.Add(new User
            {
                UserID = 1,
                FirstName = "Vinoth",
                LastName = "Kannan",
                EmployeeID = "484555",
                TaskID = 1,
                ProjectID = 1
            });


            mockUserRepository.Setup(x => x.GetAll()).Returns(userList);

            mockProjectRepository.Setup(x => x.GetAll()).Returns(projectList);
            mockParentRepository.Setup(x => x.GetAll()).Returns(parentTaskList);

            mockTaskRepository.Setup(x => x.GetAll()).Returns(taskList);
            mockTaskRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(taskList[0]);
            mockTaskRepository.Setup(x => x.Delete(It.IsAny<int>())).Callback(() => { });//.Throws(new InvalidOperationException("DELETE"));
            mockTaskRepository.Setup(x => x.Update(It.IsAny<Task>())).Returns(taskList[0]);
            mockTaskRepository.Setup(x => x.Insert(It.IsAny<Task>())).Returns(taskList[0]);
            mockTaskRepository.Setup(x => x.Save()).Callback(() => { });

            controller = new TaskController();
            controller.parentRepository = mockParentRepository.Object;
            controller.userRepository = mockUserRepository.Object;
            controller.projectRepository = mockProjectRepository.Object;
            controller.taskRepository = mockTaskRepository.Object;

        }



        [TestMethod]
        public void CreateTaskTest()
        {
            TaskModel model = new TaskModel
            {
                TaskID = 1,
                TaskName = "Test",
                ProjectID = 1,
                ProjectName = "Test Project",
                IsParentTask = true,
                ParentTaskID = 1,
                ParentTaskName = "Test Parent Task",
                Priority = 1,
                Status = "Completed",
                UserID = 1,
                UserName = "Teset user",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };
            TaskModel model1 = new TaskModel
            {
                TaskID = 1,
                TaskName = "Test",
                ProjectID = 1,
                ProjectName = "Test Project",
                IsParentTask = false,
                ParentTaskID = 1,
                ParentTaskName = "Test Parent Task",
                Priority = 1,
                Status = "Completed",
                UserID = 1,
                UserName = "Teset user",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };
            var result = controller.CreateTask(model);
            var result1 = controller.CreateTask(model1);
        }

        [TestMethod()]
        public void GetTasksTest()
        {
            var result = controller.GetTasks();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DeleteTaskTest()
        {
            var result = controller.DeleteTask(1007);
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void UpdateTaskTest()
        {
            TaskModel model = new TaskModel();
            var result = controller.UpdateTask(model);
            Assert.IsTrue(true);

        }
    }
}
