using EVTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVTest.DAL.Common
{
    public abstract class DataServiceBase<T> where T : class
    {
            /// <summary>
            /// Gets the data context.
            /// </summary>
            /// <value>
            /// The data context.
            /// </value>
            public EVTestEntities DataContext
            {
                get;
                set;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DataServiceBase{T}" /> class.
            /// </summary>
            /// <param name="context">The context.</param>
            public DataServiceBase(EVTestEntities context)
            {
                this.DataContext = context;
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
            /// <param name="request">The entitiy.</param>
            public abstract T Update(T request);

        }
}