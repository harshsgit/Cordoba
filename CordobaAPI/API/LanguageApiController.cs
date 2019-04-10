using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using CordobaModels.Entities;

namespace CordobaAPI.API
{
    public class LanguageApiController: ApiController
    {
        public ILanguageService _LanguageServices;

        public LanguageApiController()
        {
            _LanguageServices =new LanguageService();
        }


        [HttpGet]
        public HttpResponseMessage GetLanguageList(int? languageId, int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _LanguageServices.GetLanguageList(languageId, StoreId, LoggedInUserId);
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

        [HttpPost]
        public HttpResponseMessage InsertOrUpdateLanguage(int StoreId, int LoggedInUserId, LanguageEntity objEntity)
        {
            try
            {
                //HttpContext.Current.Server.MapPath("~/Content/NewsImages)
                int result = _LanguageServices.InsertOrUpdateLanguage(StoreId, LoggedInUserId, objEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage DeleteLanguage(int StoreId, int LoggedInUserId, int languageId)
        {
            try
            {
                int result = _LanguageServices.DeleteLanguage(StoreId, LoggedInUserId, languageId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}