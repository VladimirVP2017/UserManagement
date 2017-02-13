﻿using Microsoft.WindowsAzure.Storage;
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
        
        

        public IEnumerable<User> GetAllUsers()
        {
            List<User> users = new List<User>();

           
            // Retrieve a reference to the table.
            var table = new DAL.DBUtils().Table;

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

           
            List<User> _records = new List<User>();
            User user;
            var table = new DAL.DBUtils().Table;
            TableOperation retrieveOperation = TableOperation.Retrieve<User>("User", id.ToString());

            try
            {
                // Execute the retrieve operation.
                TableResult retrievedResult = table.Execute(retrieveOperation);

                 user = retrievedResult.Result as User;
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/User
        public string CreateUser(string userDomain, string userId, string firstName, string lastName, int age)
        {
            string sResponse = "";
            var table = new DAL.DBUtils().Table;
            

            // Create our user

            // Create the entity with a partition key for user and a row
            // Row should be unique within that partition
            User item = new User(int.Parse(userId), userDomain);

            item.FirstName = firstName;
            item.LastName = lastName;
            item.Age = age;

           
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
          

          

            // Create the entity with a partition key for user and a row
            // Row should be unique within that partition
            User item = new User(int.Parse(userId), userDomain);

            item.FirstName = firstName;
            item.LastName = lastName;
            item.Age = age;
            item.ETag = etag;
            //Obtain a reference to CloudTable object.
            var table = new DAL.DBUtils().Table;
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
