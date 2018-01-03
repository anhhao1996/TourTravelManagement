using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;

namespace MODEL
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// The context object for the database
        /// </summary>
        /// 
        private ObjectContext _context;

        /// <summary>
        /// The IObjectSet that represents the current entity.
        /// </summary>
        private IObjectSet<T> _objectSet;

        /// <summary>
        /// Initializes a new instance of the GenericRepository class
        /// </summary>
        /// <param name="context">The Entity Framework ObjectContext</param>
        public GenericRepository(ObjectContext context)
        {
            _context = context;
            _objectSet = _context.CreateObjectSet<T>();
        }

        public GenericRepository()
        {
            _context = ((IObjectContextAdapter)new tourdulichEntities()).ObjectContext;
            _objectSet = _context.CreateObjectSet<T>();
        }

        /// <summary>
        /// Gets all records as an IQueryable
        /// </summary>
        /// <returns>An IQueryable object containing the results of the query</returns>
        public IQueryable<T> GetQuery()
        {
            return _objectSet;
        }

        /// <summary>
        /// Gets all records as an IEnumberable
        /// </summary>
        /// <returns>An IEnumberable object containing the results of the query</returns>
        public IEnumerable<T> GetAll()
        {
            return GetQuery().AsEnumerable();
        }

        /// <summary>
        /// Finds a record with the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A collection containing the results of the query</returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _objectSet.Where<T>(predicate);
        }

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public T Single(Expression<Func<T, bool>> predicate)
        {
            return _objectSet.Single<T>(predicate);
        }

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public T First(Expression<Func<T, bool>> predicate)
        {
            return _objectSet.First<T>(predicate);
        }

        /// <summary>
        /// Deletes the specified entitiy
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <exception cref="ArgumentNullException"> if <paramref name="entity"/> is null</exception>
        public bool Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _objectSet.Attach(entity);
            _objectSet.DeleteObject(entity);
            _context.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Deleted);
            try
            {
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //finally
            //{
            //    Dispose();
            //}
        }

        /// <summary>
        /// Adds the specified entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <exception cref="ArgumentNullException"> if <paramref name="entity"/> is null</exception>
        public bool Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _objectSet.AddObject(entity);
            _context.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Added);
            try
            {
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //finally
            //{
            //    Dispose();
            //}
        }
        /// <summary>
        /// Attaches the specified entity
        /// </summary>
        /// <param name="entity">Entity to attach</param>
        public bool Attach(T entity)
        {
            _objectSet.Attach(entity);
            _context.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
            try
            {
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //finally
            //{
            //    Dispose();
            //}
        }

        public bool Update(T entity)
        {
            _context.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
            try
            {
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //finally
            //{
            //    Dispose();
            //}
        }

        /// <summary>
        /// Saves all context changes
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Releases all resources used by the WarrantManagement.DataExtract.Dal.ReportDataBase
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the WarrantManagement.DataExtract.Dal.ReportDataBase
        /// </summary>
        /// <param name="disposing">A boolean value indicating whether or not to dispose managed resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}
