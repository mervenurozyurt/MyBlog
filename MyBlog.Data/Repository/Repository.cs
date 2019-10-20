using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private MyBlogEntities _db;

        public Repository(MyBlogEntities db)
        {
            _db = db;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _db.Set<T>().Any(predicate);
        }

        public void Delete(int id)
        {
            var result = _db.Set<T>().Find(id);
            _db.Set<T>().Remove(result);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public T Get(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _db.Set<T>().FirstOrDefault(predicate);
        }

        public List<T> List()
        {
            return _db.Set<T>().ToList();
        }

        public T Save(T obj)
        {
            _db.Set<T>().Add(obj);
            return obj;
        }

        public List<T> Search(Expression<Func<T, bool>> predicate)
        {
            //select * from T where column1= and/or column2=
            return _db.Set<T>().Where(predicate).ToList();
        }

        public T Update(T obj)
        {
            _db.Entry<T>(obj).State = System.Data.Entity.EntityState.Modified;
            return obj;
        }
    }
}
