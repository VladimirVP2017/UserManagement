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
        string _connectionString; //= ConfigurationManager.ConnectionStrings["StorageConnection"].ToString();
        

        public IEnumerable<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            _connectionString = ConfigurationManager.ConnectionStrings["StorageConnection"].ToString();
            List<User> _records = new List<User>();

            //Create a storage account object.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("users");

            TableQuery<User> query = new TableQuery<User>();


            foreach (var item in table.ExecuteQuery(query))
            {
                users.Add(new User()
                {
                    Age = item.Age,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    RowKey = item.RowKey,
                });
            }

            return (IEnumerable<User>) users;
        }

        public IHttpActionResult GetUser(int id)
        {

             _connectionString = ConfigurationManager.ConnectionStrings["StorageConnection"].ToString();
            List<User> _records = new List<User>();

            //Create a storage account object.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("users");

            TableOperation retrieveOperation = TableOperation.Retrieve<User>("User", id.ToString());

            // Execute the retrieve operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);


            // Create the table if it doesn't exist.
            table.CreateIfNotExists();
            User user = retrievedResult.Result as User;
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/Player
        public string CreateUser(string userDomain, string userId, string firstName, string lastName, int age)
        {
            string sResponse = "";
            _connectionString = ConfigurationManager.ConnectionStrings["StorageConnection"].ToString();

            // Create our player

            // Create the entity with a partition key for sport and a row
            // Row should be unique within that partition
            User item = new User(int.Parse(userId), userDomain);

            item.FirstName = firstName;
            item.LastName = lastName;
            item.Age = age;

            //Create a storage account object.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("users");

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(item);

            try
            {
                // Execute the insert operation.
                table.Execute(insertOperation);

                sResponse = "OK";
            }
            catch (Exception ex)
            {
                sResponse = "Failed: " + ex.ToString();
            }
            return sResponse;
        }

        [HttpPut]
        public string UpdateUser(string userDomain, string userId, string firstName, string lastName, int age, string etag)
        {
            string sResponse = "";
            _connectionString = ConfigurationManager.ConnectionStrings["StorageConnection"].ToString();

            // Create our player

            // Create the entity with a partition key for sport and a row
            // Row should be unique within that partition
            User item = new User(int.Parse(userId), userDomain);

            item.FirstName = firstName;
            item.LastName = lastName;
            item.Age = age;
            item.ETag = etag;
            //Create a storage account object.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("users");

            // Create the TableOperation object that inserts the customer entity.
            TableOperation updateOperation = TableOperation.Replace(item);

            try
            {
                // Execute the insert operation.
                table.Execute(updateOperation);

                sResponse = "OK";
            }
            catch (Exception ex)
            {
                sResponse = "Failed: " + ex.ToString();
            }
            return sResponse;

        }

    }
}
