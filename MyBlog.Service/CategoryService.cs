using MyBlog.Data;
using MyBlog.Data.UnitOfWork;
using MyBlog.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class CategoryService : BaseService
    {
        public List<CategoryDTO> GetCategories()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var result = uow.Categories.Search(x => x.RecordStatusId == 1);

                List<CategoryDTO> list = new List<CategoryDTO>();

                foreach (var item in result)
                {
                    CategoryDTO obj = new CategoryDTO
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        NumberOfBlogs = item.Blogs.Count
                    };

                    list.Add(obj);
                }

                return list.OrderBy(x => x.CategoryName).ToList();
            }
        }

        public int Save(CategoryDTO obj)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    obj.RecordStatusId = Convert.ToByte(Enums.RecordStatus.Aktif);
                    var entity = uow.Categories.Save(Mapper.Map<CategoryDTO, Category>(obj));

                    uow.Commit();

                    return entity.CategoryId;
                }
                catch (Exception)
                {
                    uow.RollBack();
                    return 0;
                }
            }
        }

        public bool Update(CategoryDTO obj)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    var entity = uow.Categories.Update(Mapper.Map<CategoryDTO, Category>(obj));

                    return uow.Commit();
                }
                catch (Exception)
                {
                    uow.RollBack();
                    return false;
                }
            }
        }

        public CategoryDTO Get(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    var entity = uow.Categories.Get(id);

                    CategoryDTO category = Mapper.Map<Category, CategoryDTO>(entity);
                    category.NumberOfBlogs = entity.Blogs.Count;
                    category.RecordStatusName = entity.RecordStatus.RecordStatusName;

                    return category;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<CategoryDTO> GetTopFiveCategories()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var result = uow.Categories
                    .Search(x => x.RecordStatusId == 1)
                    .OrderByDescending(x => x.Blogs.Count)
                    .Take(5)
                    .ToList();

                List<CategoryDTO> list = new List<CategoryDTO>();

                foreach (var item in result)
                {
                    CategoryDTO obj = new CategoryDTO
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        NumberOfBlogs = item.Blogs.Count
                    };

                    list.Add(obj);
                }

                return list.OrderBy(x => x.CategoryName).ToList();
            }
        }
    }
}
