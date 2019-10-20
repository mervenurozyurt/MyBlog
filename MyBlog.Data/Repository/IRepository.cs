using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> Search(Expression<Func<T, bool>> predicate);
        List<T> List();
        T Save(T obj);
        T Update(T obj);
        void Delete(int id);
        T Get(int id);
        T Get(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);
    }
}
