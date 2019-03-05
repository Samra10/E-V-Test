using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVTest.Model;
using EVTest.DAL.Common;

namespace EVTest.BLL.Common
{
    public abstract class ManagerBase<T> where T : class
    {
        /// <summary>
        /// Gets the data context.
        /// </summary>
        public EVTestEntities DataContext
        {
            get;
            set;
        }
        public ManagerBase()
        {
            EVTestEntities context = new EVTestEntities();
            this.DataContext = context;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ManagerBase(EVTestEntities context)
        {
            this.DataContext = context;
            //Can Set DataServiceBase using Factory
        }

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public abstract T Add(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public abstract void Delete(T entity);

        /// <summary>
        /// Deletes the specified entity id.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        public abstract void Delete(int entityId);

        /// <summary>
        /// Gets the entity by id.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>Object of T</returns>
        public abstract T GetById(int entityId);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>Set of All entities</returns>
        public abstract List<T> GetAll();

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="user">The entity.</param>
        public abstract T Update(T user);

    }
}