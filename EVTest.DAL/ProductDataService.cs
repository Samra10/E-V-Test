using EVTest.DAL.Common;
using EVTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;

namespace EVTest.DAL
{
    public class ProductDataService : DataServiceBase<Product>
    {
        
    	public ProductDataService(EVTestEntities dataContext):base(dataContext)
    	{
            this.DataContext = dataContext;
        }

        /// <summary>
    	/// Gets the specified entity by Id.
    	/// </summary>
    	/// <param name="id">The Product Id.</param>
    	public override Product GetById(int id)
        {
            try
            {
                return DataContext.Products.FirstOrDefault(entity => entity.ProductID == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
    	/// Get All Products.
    	/// </summary>
    	public override List<Product> GetAll()
        {
            try
            {
                return DataContext.Products.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
    	/// Adds the specified entity.
    	/// </summary>
    	/// <param name="pr">The product entity.</param>
    	public override Product Add(Product pr)
        {
            try
            {
                DataContext.Products.Add(pr);
                DataContext.SaveChanges();
                return pr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
    	/// Deletes the specified entity.
    	/// </summary>
    	/// <param name="pr">The product entity.</param>
    	public override void Delete(Product pr)
        {
            try
            {
                DataContext.Products.Remove(pr);
                DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
    	/// Deletes the entity by Id.
    	/// </summary>
    	/// <param name="id">The product Id.</param>
    	public override void Delete(int id)
        {
            try
            {
                var pr = GetById(id);
                DataContext.Products.Remove(pr);
                DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
    	/// Updates the specified entity.
    	/// </summary>
    	/// <param name="pr">The product entity.</param>
    	public override Product Update(Product pr)
        {
            try
            {
                var _pr = GetById(pr.ProductID);
                /*if(_pr == null)
                {
                    throw new InvalidOperationException("Product Not found");
                }
                _pr.LastUpdate = pr.LastUpdate;
                _pr.Photo = pr.Photo;
                _pr.Price = pr.Price;
                _pr.ProductName = pr.ProductName;*/
                //DataContext.Products.AddOrUpdate(pr);
                //DataContext.Products.Add(pr);
                //DataContext.Entry(pr).State = System.Data.Entity.EntityState.Modified;

                DataContext.Entry(_pr).CurrentValues.SetValues(pr);
                DataContext.SaveChanges();
                return pr;
            }
            catch (Exception ex)
            {
                //ExceptionHandler.LogException(ex, System.Reflection.MethodInfo.GetCurrentMethod().Name, ExceptionHandler.LogThreshold.ERROR);
                throw ex;
            }
        }
    }
}