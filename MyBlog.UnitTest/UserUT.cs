using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBlog.DTO;
using MyBlog.Service;

namespace MyBlog.UnitTest
{
    [TestClass]
    public class UserUT
    {
        private UserService service;

        [TestInitialize]
        public void Setup()
        {
            service = new UserService();
        }

        [TestMethod]
        public void Register()
        {
            UserDTO user = new UserDTO
            {
                FirstName = "Büşra Nur",
                LastName = "Baykal",
                TownId = 1421,
                MobilePhone = "05412279878",
                EmailAddress = "b.nurbaykal@hotmail.com",
                Password = "1234",
                UserTypeID = Convert.ToByte(Enums.UserType.Member)
            };

            var result = service.Register(user);

            Assert.IsTrue(result);
        }
    }
}
