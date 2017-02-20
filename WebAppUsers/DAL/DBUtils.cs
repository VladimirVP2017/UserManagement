using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebAppUsers.Models;

namespace WebAppUsers.DAL
{
    public class DBUtils : IDisposable
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;
        public DBUtils()
        {
            //Get the Storage Account from the conenction string
            storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnection"].ToString());
            //Create a Table Client Object
            tableClient = storageAccount.CreateCloudTableClient();
            //Create Table if it does not exist
            table = tableClient.GetTableReference("users");
            table.CreateIfNotExists();
        }


        public TableResult Create(User user)
        {
            TableOperation insertOperation = TableOperation.Insert(user);
            // Execute the insert operation.
            return table.Execute(insertOperation);
        }
        public TableResult Update(User user)
        {
            TableOperation insertOperation = TableOperation.Replace(user);
            // Execute the insert operation.
            return table.Execute(insertOperation);
        }
        public IEnumerable<User> GetAll()
        {
            // Retrieve a reference to the table.
            TableQuery<User> query = new TableQuery<User>();
            return (IEnumerable<User>)table.ExecuteQuery(query);
        }

        public User GetById(int id, string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<User>(rowKey, id.ToString());
            return (User)table.Execute(retrieveOperation).Result;
        }

        public void Dispose()
        {

        }
    }  
}