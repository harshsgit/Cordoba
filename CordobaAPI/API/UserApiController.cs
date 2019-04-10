using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class UserApiController : ApiController
    {

        public IUserServices _UserServices;

        public UserApiController()
        {
            _UserServices = new UserServices();
        }

        [HttpGet]
        public HttpResponseMessage GetUserList(int LoggedInUserId, int storeId)
        {
            try
            {
                var result = _UserServices.GetUserList(LoggedInUserId, storeId);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public HttpResponseMessage GetUserDetail(int LoggedInUserId, int storeId, int UserID = 0)
        {
            try
            {
                var result = _UserServices.GetUserDetail(LoggedInUserId, storeId, UserID);
          
                if (result != null)
                {
                    result.password = Security.Decrypt(result.password);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        public HttpResponseMessage CreateOrUpdateUser(int LoggedInUserId, int storeId, UserEntity UserModel)
        {
            try
            {
                UserModel.password = Security.Encrypt(UserModel.password);
                var result = _UserServices.CreateOrUpdateUser(LoggedInUserId, storeId, UserModel);
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception)
            {

                throw;
            }

        }

        //[HttpGet]
        //public HttpResponseMessage DeleteUserDetail(int LoggedInUserId, int UserID = 0)
        //{
        //    try
        //    {
        //        var result = _UserServices.DeleteUserDetail(LoggedInUserId, UserID);
        //        return Request.CreateResponse(HttpStatusCode.OK, result);

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        [HttpGet]
        public HttpResponseMessage DeleteUserDetail(int LoggedInUserId, int storeId, int UserID = 0)
        {
            try
            {
                var result = _UserServices.DeleteUserDetail(LoggedInUserId, storeId, UserID);
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpGet]

        // GET: api/UserApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UserApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UserApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UserApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserApi/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        public UserEntity AuthenticUserDetail(UserEntity model)
        {
            try
            {
                var result = _UserServices.AuthenticUserDetail(model);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpGet]
        //public UserEntity GetAuthenticUserDetail(UserEntity model)
        //{
        //    try
        //    {
        //        var result = _UserServices.IsAuthenticUser(model);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
