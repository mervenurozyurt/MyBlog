using MyBlog.Data;
using MyBlog.Data.UnitOfWork;
using MyBlog.DTO;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyBlog.Service
{
    public class UserService : BaseService
    {
        public bool Register(UserDTO obj)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    obj.CreatedDate = DateTime.Now;
                    obj.IsEmailVerified = false;
                    obj.Token = Guid.NewGuid().ToString();
                    obj.TokenValidUntil = DateTime.Now.AddDays(1);
                    obj.RecordStatusId = Convert.ToByte(Enums.RecordStatus.Aktif);

                    var entity = Mapper.Map<UserDTO, User>(obj);

                    uow.Users.Save(entity);

                    bool result = uow.Commit();

                    if (result)
                    {
                        using (MailService mailService = new MailService())
                        {
                            MailDTO mail = new MailDTO
                            {
                                To = obj.EmailAddress,
                                Subject = "E-posta doğrulama",
                                Body = GetVerifyMailTemplate("http://localhost:58203/Account/Verify/" + obj.Token)
                            };

                            return mailService.SendMail(mail);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    uow.RollBack();
                    return false;
                }
            }
        }

        public List<UserDTO> List()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    var result = uow.Users.List();

                    List<UserDTO> list = new List<UserDTO>();

                    foreach (var item in result)
                    {
                        UserDTO obj = new UserDTO
                        {
                            UserId = item.UserId,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            CityName = item.Town.City.CityName,
                            TownName = item.Town.TownName,
                            EmailAddress = item.EmailAddress,
                            IsEmailVerified = item.IsEmailVerified,
                            CreatedDate = item.CreatedDate,
                            UserTypeName=item.UsersType.UserTypeName
                            
                        };

                        list.Add(obj);
                    }

                    return list;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public UserDTO Get(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    var result = uow.Users.Get(id);
                    UserDTO user = Mapper.Map<User, UserDTO>(result);

                    user.CityId = result.Town.CityID;
                    user.CityName = result.Town.City.CityName;
                    user.TownName = result.Town.TownName;
                    user.UserTypeName = result.UsersType.UserTypeName;
                    user.RecordStatusName = result.RecordStatus.RecordStatusName;

                    return user;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool Update(UserDTO obj)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    var entity = Mapper.Map<UserDTO, User>(obj);
                 
                    uow.Users.Update(entity);

                    return uow.Commit();
                }
                catch (Exception ex
    )
                {
                    uow.RollBack();
                    return false;
                }
            }
        }

        public bool Verify(string token)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    var result = uow.Users.Get(x => x.Token == token && x.IsEmailVerified == false);

                    if (result != null)
                    {
                        if (result.TokenValidUntil.Value <= DateTime.Now.AddDays(1))
                        {
                            result.IsEmailVerified = true;
                            uow.Users.Update(result);
                            return uow.Commit();
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    uow.RollBack();
                    return false;
                }
            }
        }

        public UserDTO Authenticate(string email, string password)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var result = uow.Users.Get(
                 x =>
                 x.EmailAddress == email &&
                 x.Password == password &&
                 x.IsEmailVerified == true);

                if (result == null)
                {
                    return null;
                }

                return this.Get(result.UserId);
            }
        }

        private string GetVerifyMailTemplate(string url)
        {
            var template = File.ReadAllText(@"C:\Users\bnb\Desktop\verify.txt");

            template = template.Replace("#VerifyUrl#", url);

            return template;
        }
    }
}
