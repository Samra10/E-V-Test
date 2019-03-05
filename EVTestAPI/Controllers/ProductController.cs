using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EVTest.BLL;
using EVTest.Model;
using System.Web.Http.Description;

namespace EVTestAPI.Controllers
{
    public class ProductController : ApiController
    {
        ProductManagerBase _productManagerBase = new ProductManagerBase();

        // GET api/values
        public IEnumerable<Product> Get()
        {
            return _productManagerBase.GetAll();
        }

        [ResponseType(typeof(Product))]
        public Product GetProduct(int id)
        {
            return _productManagerBase.GetById(id);
        }

        [HttpPost]
        public void PostProduct([FromBody]Product pr)
        {
            _productManagerBase.Add(pr);
        }

        [HttpPut]
        public void putProduct (Product pr)
        {
            _productManagerBase.Update(pr);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _productManagerBase.Delete(id);
        }
    }
}
