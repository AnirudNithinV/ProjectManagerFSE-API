using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WebAPI.Repository;
using WebAPI;
using WebAPI.Models;

namespace ProjectMgmt.UnitTest.Harness
{
    [TestClass()]
    public class ProjectControllerTests
    {
        Mock<IRepository<User>> mockUserRepository = new Mock<IRepository<User>>();
        Mock<IRepository<Project>> mockProjectRepository = new Mock<IRepository<Project>>();
        ProjectController controller;

        [TestInitialize]
        public void InitializeTest()
        {

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
                FirstName = "Vediyappan",
                LastName = "K",
                TaskID = 1,
                EmployeeID = "694221",
                ProjectID = 1
            });

            mockUserRepository.Setup(x => x.GetAll()).Returns(userList);

            mockProjectRepository.Setup(x => x.GetAll()).Returns(projectList);
            mockProjectRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(projectList[0]);
            mockProjectRepository.Setup(x => x.Delete(It.IsAny<int>())).Callback(() => { });//.Throws(new InvalidOperationException("DELETE"));
            mockProjectRepository.Setup(x => x.Update(It.IsAny<Project>())).Returns(projectList[0]);
            mockProjectRepository.Setup(x => x.Insert(It.IsAny<Project>())).Returns(projectList[0]);
            mockProjectRepository.Setup(x => x.Save()).Callback(() => { });

            controller = new ProjectController();
            controller.userRepository = mockUserRepository.Object;
            controller.projectRepository = mockProjectRepository.Object;

        }

        [TestMethod()]
        public void GetProjectsTest()
        {
            var result = controller.GetProjects();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DeleteProjectTest()
        {
            controller.DeleteProjectById(1);
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void PostProjectTest()
        {
            var result = controller.CreateProject(new ProjectDBModel { ProjectID = 1, ProjectTitle = "HIT 1402", Priority = 1 });
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void PutProjectTest()
        {
            var result = controller.UpdateProject(new Project { ProjectID = 1, ProjectTitle = "HIT 1402", Priority = 1 });
            Assert.IsNotNull(result);
        }
    }
}