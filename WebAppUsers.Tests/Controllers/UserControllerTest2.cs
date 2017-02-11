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
    public class UserControllerTest2
    {
        User[] testUsers  = new User[]
           {
            new User { Id = 1, FirstName = "Boris", LastName = "Eltsin", Age =55 },
            new User { Id = 2, FirstName = "Leonid", LastName = "Kravchuk", Age = 44 },
            new User { Id = 3, FirstName = "Stanislav", LastName = "Shushkevich", Age =47 }
           };
        public UserControllerTest2()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
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
        public void GetAllUser_ShouldReturnAllProducts()
        {
            
            var controller = new UserController();

            var result = controller.GetAllProducts();
            Assert.AreEqual(testUsers.ToArray<User>().Length, result.ToArray<User>().Length);
        }


        [TestMethod]
        public void GetUser_ShouldReturnCorrectProduct()
        {
           
            var controller = new UserController();

            var result = controller.GetUser(3) as OkNegotiatedContentResult<User>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testUsers[3].FirstName, result.Content.FirstName);
        }



    }
}
