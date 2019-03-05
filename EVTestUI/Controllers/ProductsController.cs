using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EVTest.Model;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace EVTestUI.Controllers
{
    public class ProductsController : Controller
    {
        private EVTestEntities db = new EVTestEntities();

        // GET: Products
        string Baseurl = "http://localhost:14518/api/product";
        public async Task<ActionResult> Index()
        {
            List<Product> ProductInfo = await GetAllData();

            
                //returning the Product list to view  
                return View(ProductInfo);
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            {
                Product ProductInfo = new Product();

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GETProduct using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync(Baseurl + "/" + id.ToString());

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Product Entity  
                        ProductInfo = JsonConvert.DeserializeObject<Product>(EmpResponse);

                    }
                    //returning the Product to view  
                    return View(ProductInfo);
                }
            }
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create(Product pr)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Product>("product", pr);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(pr);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            Product pr = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                //HTTP GET
                var responseTask = client.GetAsync("product?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    pr = readTask.Result;
                }
            }
            pr.LastUpdate = DateTime.Now;
            return View(pr);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product pr)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Product>("product", pr);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(pr);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            Product pr = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                //HTTP GET
                var responseTask = client.GetAsync("product?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    pr = readTask.Result;
                }
            }
            return View(pr);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("product/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> ExportToExcel()
        {
            List<Product> pr = await GetAllData();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Products Report";
            ws.Cells["B1"].Value = "Products";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Reprot1";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = String.Format("{0:dd MMMM yyyy} at {0:H: mm tt}",DateTimeOffset.Now);

            ws.Cells["A6"].Value = "ProductID";
            ws.Cells["B6"].Value = "ProductName";
            ws.Cells["C6"].Value = "Price";
            ws.Cells["D6"].Value = "LastUpdate";

            int rowStart = 8;
            foreach (var item in pr)
            {
                ws.Cells[String.Format("A{0}", rowStart)].Value = item.ProductID;
                ws.Cells[String.Format("B{0}", rowStart)].Value = item.ProductName;
                ws.Cells[String.Format("C{0}", rowStart)].Value = item.Price;
                ws.Cells[String.Format("D{0}", rowStart)].Value = item.LastUpdate.ToString();
                ++rowStart;
            }

            ws.Cells["A:AZ0"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-ooficedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

            return RedirectToAction("Index");
        }

        private async Task<List<Product>> GetAllData()
        {
            List<Product> ProductInfo = new List<Product>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GET using HttpClient  
                HttpResponseMessage Res = await client.GetAsync(Baseurl);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Product list  
                    ProductInfo = JsonConvert.DeserializeObject<List<Product>>(EmpResponse);

                }

            }
            return ProductInfo;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
