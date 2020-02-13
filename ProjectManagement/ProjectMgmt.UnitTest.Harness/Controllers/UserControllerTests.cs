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

namespace ProjectMgmt.UnitTest.Harness
{
    [TestClass()]
    public class UserControllerTests
    {
        Mock<IRepository<User>> mockUserRepository = new Mock<IRepository<User>>();

        UserController controller;

        [TestInitialize]
        public void InitializeTest()
        {
            List<User> userList = new List<User>();
            userList.Add(new User
            {
                UserID = 1,
                FirstName = "Vediyappan",
                LastName = "Kumaresan",
                EmployeeID = "694221",
                TaskID = 1,
                ProjectID = 1
            });


            mockUserRepository.Setup(x => x.GetAll()).Returns(userList);

            mockUserRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(userList[0]);
            mockUserRepository.Setup(x => x.Delete(It.IsAny<int>())).Callback(() => { });//.Throws(new InvalidOperationException("DELETE"));
            mockUserRepository.Setup(x => x.Update(It.IsAny<User>())).Returns(userList[0]);
            mockUserRepository.Setup(x => x.Insert(It.IsAny<User>())).Returns(userList[0]);
            mockUserRepository.Setup(x => x.Save()).Callback(() => { });

            controller = new UserController();
            controller.userRepository = mockUserRepository.Object;
        }



        [TestMethod()]
        public void GetUsersTest()
        {
            var result = controller.GetUsers();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DeleteUserTest()
        {
            var result = controller.DeleteUser(1);
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void PostUserTest()
        {
            var result = controller.CreateUser(new User
            {
                UserID = 1,
                FirstName = "Vediyappan",
                LastName = "Kumar",
                EmployeeID = "694221",
                TaskID = 1,
                ProjectID = 1
            });
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void PutUserTest()
        {
            var result = controller.UpdateUser(new User
            {
                UserID = 1,
                FirstName = "Vediyappan",
                LastName = "K",
                EmployeeID = "694221",
                TaskID = 1,
                ProjectID = 1
            });
            Assert.IsNotNull(result);
        }
    }
}