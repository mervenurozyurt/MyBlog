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
    public class CommentService:BaseService
    {
        public List<CommentDTO> List(int blogId)
        {
            using (UnitOfWork uow=new UnitOfWork())
            {
                try
                {
                    var entities = uow.Comments.Search(x=>x.BlogId==blogId && x.IsConfirmed==false);

                    List<CommentDTO> list = new List<CommentDTO>();

                    foreach (var item in entities)
                    {
                        CommentDTO commentDTO = new CommentDTO
                        {
                            BlogId = item.BlogId,
                            CommentContent = item.CommentContent,
                            CommentId = item.CommentId,
                            CreatedDate = item.CreatedDate,
                            IsConfirmed = item.IsConfirmed,
                            UserId = item.UserId,
                            BlogName=item.Blog.Title,
                            UserName=item.User.FirstName+" "+item.User.LastName
                        };

                        list.Add(commentDTO);
                    }

                    return list;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public bool Confirm(CommentDTO obj)
        {
            using (UnitOfWork uow=new UnitOfWork())
            {
                try
                {
                    obj.IsConfirmed = true;

                    uow.Comments.Update(Mapper.Map<CommentDTO, Comment>(obj));

                    return uow.Commit();
                }
                catch (Exception ex)
                {
                    uow.RollBack();
                    return false;
                }
            }

        }

        public CommentDTO Get(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    var comment = uow.Comments.Get(id);

                    return Mapper.Map<Comment, CommentDTO>(comment);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
