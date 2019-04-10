using CordobaCommon;
using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Formatting;
using CordobaModels;

namespace CordobaWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Login
        [HttpGet]
        public ActionResult Login()
        {
            ProjectSession.AdminLoginSession = null;
            return View(new UserEntity());
        }

        [HttpPost]
        // GET: Admin/Details/5
        public async Task<ActionResult> Login(UserEntity model)
        {            
            var apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"].ToString();
            model.password = Security.Encrypt(model.password);
            var response = await HttpClientPostPassEntityReturnEntity<UserEntity, UserEntity>(model, apiUrl + "UserApi/AuthenticUserDetail");
            if (response != null && response.user_id > 0)
            {
                ProjectSession.AdminLoginSession = response;
                Session.Add("AdminUserId", response.user_id);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public static async Task<O> HttpClientPostPassEntityReturnEntity<O, I>(I value, string uri)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync(uri, value);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<O>();
                return (O)Convert.ChangeType(result, typeof(O));
            }

            return default(O);
        }

        public static async Task<T> HttpClientRequestResponse<T>(T value, string uri)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<T>();
                return (T)Convert.ChangeType(result, typeof(T));
            }

            return default(T);
        }

    }
}
