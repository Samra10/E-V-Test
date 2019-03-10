using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVTest.BLL.Common;
using EVTest.Model;
using EVTest.DAL;

namespace EVTest.BLL
{
    public class ProductManagerBase : ManagerBase<Product>
    {
        private ProductDataService _productDataService;

        public ProductManagerBase()
        {
            EVTestEntities entities = new EVTestEntities();
            _productDataService = new ProductDataService(entities);
        }
        public ProductManagerBase(EVTestEntities entities):base(entities)
    	{
            _productDataService = new ProductDataService(entities);
        }

        /// <summary>
    	/// Gets the specified entity by ID.
    	/// </summary>
    	/// <param name="id">The Product ID.</param>
    	public override Product GetById(int id)
        {
            return _productDataService.GetById(id);
        }

        /// <summary>
    	/// Gets All.
    	/// </summary>
    	public override List<Product> GetAll()
        {
            return _productDataService.GetAll();
        }

        /// <summary>
    	/// Adds the specified entity.
    	/// </summary>
    	/// <param name="pr">The Product entity.</param>
    	public override Product Add(Product pr)
        {
            _productDataService.Add(pr);
            return pr;
        }

        /// <summary>
    	/// Deletes the specified entity.
    	/// </summary>
    	/// <param name="pr">The product entity.</param>
    	public override void Delete(Product pr)
        {
            _productDataService.Delete(pr);
        }

        /// <summary>
    	/// Deletes the entity by Id.
    	/// </summary>
    	/// <param name="id">The Product Id.</param>
    	public override void Delete(int id)
        {
            _productDataService.Delete(id);
        }

        /// <summary>
    	/// Updates the specified entity.
    	/// </summary>
    	/// <param name="pr">The product entity.</param>
    	public override Product Update(Product pr)
        {
            _productDataService.Update(pr);
            return pr;
        }

        public List<Product> searchByName(string name)
        {
            return _productDataService.searchByName(name);
        }
    }
}