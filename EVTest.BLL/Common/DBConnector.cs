using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVTest.Model;
using EVTest.DAL.Common;

namespace EVTest.BLL.Common
{
    public class DBConnector : IDisposable
    {
        private EVTestEntities _dataContext;
        private DBConnection _dbConnection;

        private DBConnection dbConnection
        {
            get
            {
                if (_dbConnection == null)
                    return new DBConnection();
                return _dbConnection;
            }
            set
            {
                _dbConnection = value;
            }
        }


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

        /// <summary>
        /// 
        /// </summary>
        public DBConnector()
        {
            this._dataContext = dbConnection.DataContext;
        }


        /// <summary>
        /// Flushes this instance.
        /// </summary>
        public void Flush()
        {
            dbConnection.Flush();
        }

        /// <summary>
        /// Releases unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Flush();
        }
    }
}