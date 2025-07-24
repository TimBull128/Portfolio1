using BB2020MVC.Models.DataLayer;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace BB2020MVC.Models.ServiceLayer
{
    public interface IServiceLayer_Base<ReturnType>: IDataLayer<ReturnType> where ReturnType : class
    {
        ReturnType GetSingle(int id);
    }
    public class ServiceLayer_Base<ReturnType> : DataLayer<ReturnType>, IServiceLayer_Base<ReturnType> where ReturnType : class
    {
        public ReturnType GetSingle(int id)
        {
            return (from items in Read() where items.Equals(id) select items).First();
        }
    }
}
