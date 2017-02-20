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
        public IHttpActionResult GetAllUsers()
        {
            using (DAL.DBUtils db = new DAL.DBUtils())
            {
                try
                {
                    return Ok(db.GetAll());
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }
       


        public IHttpActionResult GetUser(int id, string partitionKey)
        {
            User user;
            using (DAL.DBUtils db = new DAL.DBUtils())
            {  
                try
                {
                    user = db.GetById(id, partitionKey);
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
        }

        // POST api/User
        public IHttpActionResult CreateUser(User user)
        {
            using (var db = new DAL.DBUtils())
            {
                try
                {
                    db.Create(user);
                    return Ok();
                }
                catch(Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

       

        public IHttpActionResult UpdateUser(User user)
        { 
            using(DAL.DBUtils db = new DAL.DBUtils())
            try
            {
             // Execute the update operation.
                db.Update(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }      
    }
}
