using EVTest.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EVTestUI.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> show(int id)
        {

            Product ProductInfo = new Product();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri("http://localhost:14518/api/product");

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GETProduct using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("http://localhost:14518/api/product" + "/" + id.ToString());

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Product Entity  
                    ProductInfo = JsonConvert.DeserializeObject<Product>(EmpResponse);

                }

                var imageData = ProductInfo.Photo;
                return File(imageData, "image/jpg");
            }
        }
    }
}