using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppUsers.Models
{
    public class User:TableEntity
    {
     
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public User(int userId, string userCategory)
        {
            this.RowKey = userId.ToString();
            this.PartitionKey = userCategory;
        }
        public User() { }

    }
}
