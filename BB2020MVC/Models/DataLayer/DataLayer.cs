using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB2020MVC.Models.DataLayer
{
    //This interface determines the base setup for the Data Layer
    public interface IDataLayer<ReturnType> where ReturnType : class
    {
        IList<ReturnType> Read();
        ReturnType Create(ReturnType entry);
        IList<ReturnType> CreateMulti(IList<ReturnType> list);
        ReturnType Update(ReturnType entry);
        void Delete(ReturnType ID);
        void DeleteMulti(IList<ReturnType> list);
    }
    //Function for connection details - handled throughout the Data Layer
    public class ConnDetails<ReturnType> where ReturnType : class
    {
        private readonly BB2020Entities _DataContext;
        private DbSet<ReturnType> _DbSet;
        private bool _DbSet_Set = false;
        public ConnDetails()
        {
            _DataContext = new BB2020Entities();
        }
        public void SaveChanges()
        {
            _DataContext.SaveChanges();
        }
        public void SetTable(System.Data.Entity.DbSet<ReturnType> Table)
        {
            _DbSet = Table;
            _DbSet_Set = true;
        }
        public bool TableExists() => _DbSet_Set;
        public DbSet<ReturnType> Table() => _DbSet;
        public BB2020Entities DataContext() => _DataContext;

    };

    public class DataLayer<ReturnType> : ConnDetails<ReturnType>, IDataLayer<ReturnType> where ReturnType : class
    {
        public ReturnType Create(ReturnType entry)
        {
            if (TableExists())
            {
                Table().Add(entry);
                SaveChanges();
            }
            else
            {
                throw new NotImplementedException();
            }
            return entry;
                
        }
        public IList<ReturnType> CreateMulti(IList<ReturnType> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            else if (!TableExists())
            {
                throw new NotImplementedException();
            }
            else
            {
                Table().AddRange(list);
                SaveChanges();
            }
            return list;
        }

        public void Delete(ReturnType Entry)
        {
            if (!TableExists()) 
            {
                throw new NotImplementedException();
            }
            else
            {
                Table().Remove(Entry);
                SaveChanges();
            }
        }

        public void DeleteMulti(IList<ReturnType> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            else if (!TableExists())
            {
                throw new NotImplementedException();
            }
            else
            {
                Table().RemoveRange(list);
                SaveChanges();
            }
        }

        public IList<ReturnType> Read()
        {
            if (!this.TableExists())
            {
                throw new NotImplementedException();
            }
            else
            {
                return (from item in Table() select item).ToList();
            }
        }

        public ReturnType Update(ReturnType entry)
        {
            if (!this.TableExists())
            {
                throw new NotImplementedException();
            }
            else
            {
                Table().AddOrUpdate(entry);
                SaveChanges();
                return entry;
            }
        }
    }
}
