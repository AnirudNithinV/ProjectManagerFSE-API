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
using NBench;

namespace ProjectMgmt.UnitTest.Harness
{
    [TestClass()]
    public class ParentControllerTests
    {
        Mock<IRepository<Parent_Task>> mockParentRepository = new Mock<IRepository<Parent_Task>>();
        ParentController controller;

        [TestInitialize]
        public void InitializeTest()
        {
            List<Parent_Task> parentTaskList = new List<Parent_Task>();
            parentTaskList.Add(new Parent_Task
            {
                ParentID = 1,
                ParentTask = "Parent Task1"
            });
            mockParentRepository.Setup(x => x.GetAll()).Returns(parentTaskList);

            controller = new ParentController();
            controller.parentRepository = mockParentRepository.Object;
        }


        [TestMethod()]
        public void GetParentTasksTest()
        {
            var result = controller.GetParentTasks();
            Assert.IsNotNull(result);
        }

    }
}