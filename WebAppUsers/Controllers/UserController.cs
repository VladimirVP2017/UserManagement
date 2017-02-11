using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppUsers.Models;

namespace WebAppUsers.Controllers
{
    public class UserController : ApiController
    {
        string _connectionString = ConfigurationManager.ConnectionStrings["StorageConnection"].ToString();
        User[] users = new User[]
           {
            new User { Id = 1, FirstName = "Boris", LastName = "Eltsin", Age =55 },
            new User { Id = 2, FirstName = "Leonid", LastName = "Kravchuk", Age = 44 },
            new User { Id = 3, FirstName = "Stanislav", LastName = "Shushkevich", Age =47 }
           };

        public IEnumerable<User> GetAllProducts()
        {
            return users;
        }

        public IHttpActionResult GetUser(int id)
        {
           
           
            List<User> _records = new List<User>();

            //Create a storage account object.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("users");

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();
            var user = users.FirstOrDefault((p) => p.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

            // Get All Players for a sport
            /*  TableQuery<User> query = new TableQuery<User>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userId));

              foreach (User entity in table.ExecuteQuery(query))
              {
                  _records.Add(entity);
              }

              return _records;
              */
            //return null;
        }
    

    }
}
