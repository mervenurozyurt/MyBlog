using MyBlog.Data.Repository;
using System;
using System.Data.Entity;

namespace MyBlog.Data.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private MyBlogEntities db;
        private DbContextTransaction transaction;

        public UnitOfWork()
        {
            db = new MyBlogEntities();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool Commit()
        {
            transaction = db.Database.BeginTransaction();
            int affected = db.SaveChanges();
            transaction.Commit();
            return affected > 0;
        }

        public void RollBack()
        {
            transaction.Rollback();
        }

        #region Entities

        public Repository<Blog> Blogs { get { return new Repository<Blog>(db); } }
        public Repository<Category> Categories { get { return new Repository<Category>(db); } }
        public Repository<City> Cities { get { return new Repository<City>(db); } }
        public Repository<Comment> Comments { get { return new Repository<Comment>(db); } }
        public Repository<Message> Messages { get { return new Repository<Message>(db); } }
        public Repository<NewsLetter> NewsLetters { get { return new Repository<NewsLetter>(db); } }
        public Repository<RecordStatus> RecordStatuses { get { return new Repository<RecordStatus>(db); } }
        public Repository<Town> Towns { get { return new Repository<Town>(db); } }
        public Repository<User> Users { get { return new Repository<User>(db); } }
        public Repository<UsersType> UsersTypes { get { return new Repository<UsersType>(db); } }

        #endregion
    }
}
