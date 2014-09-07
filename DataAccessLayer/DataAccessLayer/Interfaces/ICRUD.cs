using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    interface ICrud: ICreate, IRead, IUpdate, IDelete
    {
        /// <summary>
        /// Returns if active connection is open. Uses DataConnection.State == System.Data.ConnectionState.Open
        /// </summary>
        Boolean IsConnectionOpen { get; }
    }
}
