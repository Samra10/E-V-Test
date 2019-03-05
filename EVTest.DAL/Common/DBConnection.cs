using System;
using EVTest.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVTest.DAL.Common
{
    public class DBConnection : IDisposable
    {
        #region Fields

        private EVTestEntities _dataContext;

        #endregion Fields


        #region Properties

        /// <summary>
        /// Gets the data context.
        /// </summary>
        /// <value>
        /// The data context.
        /// </value>
        public EVTestEntities DataContext
        {
            get
            {
                return this._dataContext;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        public DBConnection()
        {
            this._dataContext = new EVTestEntities();

        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Flushes this instance.
        /// </summary>
        public void Flush()
        {
            //_dataContext.SaveChanges();

            _dataContext.Dispose();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Flush();
        }

        #endregion Methods
    }
}
