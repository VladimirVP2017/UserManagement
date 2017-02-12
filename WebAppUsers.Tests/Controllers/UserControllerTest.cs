using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppUsers.Controllers;
using WebAppUsers.Models;
using System.Web.Http.Results;
using System.Linq;

namespace WebAppUsers.Tests.Controllers
{
    /// <summary>
    /// Summary description for UserControllerTest2
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

            var result = controller.GetAllUsers();
            Assert.IsTrue(result.ToArray<User>().Length > 0);
        }


        [TestMethod]
        public void GetUser_ShouldReturnCorrectUser()
        {
            var controller = new UserController();
            var result = controller.GetUser(3) as OkNegotiatedContentResult<User>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testUsers[0].FirstName, result.Content.FirstName);
        }

        [TestMethod]
        public void CreateUser_ShouldReturnOk()
        {
            var controller = new UserController();
            var random = new Random();
            var max = int.MaxValue;
            var result = controller.CreateUser("User", random.Next(1, max).ToString(), "TestUserFirstName" + DateTime.Now, "TestUserLastName" + DateTime.Now, 27);
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "OK");
        }


        [TestMethod]
        public void UpdateUser_ShouldReturnOk()
        {
            var controller = new UserController();
            var random = new Random();
            var maxAge = 100;
            OkNegotiatedContentResult<User> contentResult = controller.GetUser(6) as OkNegotiatedContentResult<User>;
            var userToUpdate = contentResult.Content;
            var result = controller.UpdateUser(userToUpdate.PartitionKey, userToUpdate.RowKey, "TestUserFirstName" + DateTime.Now, "TestUserLastName" + DateTime.Now, random.Next(1, maxAge), userToUpdate.ETag);
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "OK");
        }




    }
}
