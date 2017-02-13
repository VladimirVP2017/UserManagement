using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebAppUsers.DAL
{
    public class DBUtils
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

        public CloudTable Table
        {
            get { return table; }
        }
    }
}