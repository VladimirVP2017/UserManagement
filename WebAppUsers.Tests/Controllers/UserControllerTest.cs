using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppUsers.Controllers;
using WebAppUsers.Models;
using System.Web.Http.Results;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace WebAppUsers.Tests.Controllers
{
    /// <summary>
    /// Summary description for UserControllerTest
    /// </summary>
    [TestClass]
    public class UserControllerTest
    {
        User[] testUsers  = new User[]
           {
              new User { RowKey = "3", FirstName = "Eugene", LastName = "Petrov", Age = 23 }
           };

        public UserControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }


       

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        [TestMethod]
        public void GetAllUser_ShouldReturnAllUsers()
        {
            var controller = new UserController();
            var result = controller.GetAllUsers() as OkNegotiatedContentResult<IEnumerable<User>> ;
            Assert.IsNotNull(result);
        }

        
        [TestMethod]
        public void GetUser_ShouldReturnCorrectUser()
        {
            var controller = new UserController();
            var result = controller.GetUser(3, "User") as OkNegotiatedContentResult<User>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testUsers[0].FirstName, ((User)result.Content).FirstName);
        }

        [TestMethod]
        public void CreateUser_ShouldReturnOk()
        {
            var controller = new UserController();
            var random = new Random();
            var max = int.MaxValue;
            var user = new User();
            user.PartitionKey = "User";
            user.RowKey = random.Next(1, max).ToString();
            user.FirstName = "TestUserFirstName" + DateTime.Now;
            user.LastName = "TestUserLastName" + DateTime.Now;
            user.Age = 37;
            var result = controller.CreateUser(user);
            result = result as OkResult;
            Assert.IsNotNull(result);         
        }


        [TestMethod]
        public void UpdateUser_ShouldReturnOk()
        {
            var controller = new UserController();
            OkNegotiatedContentResult<User> contentResult = controller.GetUser(6, "User") as OkNegotiatedContentResult<User>;
            var userToUpdate = contentResult.Content;
            var result = controller.UpdateUser(userToUpdate);
            result = result as OkResult;
            Assert.IsNotNull(result);
            
        }

    }
}
