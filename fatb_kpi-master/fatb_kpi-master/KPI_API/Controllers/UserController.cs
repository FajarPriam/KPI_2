using KPI_API.Models;
using KPI_API.Views_Models;
using System;
using System.Web.Http;

namespace KPI_API.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult login(ClsUser clsUser)
        {
            try
            {
                //ClsUser clsUser = new ClsUser();

                int cek = clsUser.c_login();

                return Ok(new { Status = true, StatusLogin = cek });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("getSession")]
        public IHttpActionResult getSession(ClsUser clsUser)
        {
            try
            {
                var data = clsUser.c_getUser();

                return Ok(new { Status = true, Data = data });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Error = e.ToString() });
            }
        }

        [HttpGet]
        [Route("getRole")]
        public IHttpActionResult getRole()
        {
            try
            {
                ClsUser clsUser = new ClsUser();

                var data = clsUser.c_getRole();

                return Ok(new { Status = true, Data = data });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("getUser")]
        public IHttpActionResult getUser(string user, int role)
        {
            try
            {
                ClsUser clsUser = new ClsUser();

                var data = clsUser.c_getUser();

                return Ok(new { Status = true, Data = data });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("CreateOrUpdateUser")]
        public IHttpActionResult CreateOrUpdateUser(TBL_M_USER param)
        {
            try
            {
                ClsUser clsUser = new ClsUser();
                clsUser.c_InsertOrUpdate(param);

                return Ok(new { Status = true, Message = "Insert User Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("DeleteUser/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                ClsUser clsUser = new ClsUser();
                clsUser.c_deleteUser(id);

                return Ok(new { Status = true, Message = "Delete User Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("getAllUser")]
        public IHttpActionResult getAllUser()
        {
            try
            {
                ClsUser clsUser = new ClsUser();

                var list = clsUser.c_getAllUsers();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("getUserELP")]
        public IHttpActionResult getUserELP()
        {
            try
            {
                ClsUser clsUser = new ClsUser();

                var list = clsUser.c_getUsersELP();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

    }
}
