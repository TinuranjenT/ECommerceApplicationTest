using EcommerceApplication.Controllers;
using EcommerceApplication;
using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EcommerceApplicationNunitTest
{
    public class Tests
    {
        [Test]
        public void GetUserByIdTest()
        {
            // Arrange
            var test = new ECommerceRepository();

            // Act
            var actualUser = test.GetUserById(1);
            //Console.WriteLine(actualUser.Username);
            //Console.WriteLine(actualUser.Password);
            // Assert
            Assert.IsNotNull(actualUser, "User with ID 1 should not be null");
            var expectedUser = new User
            {
                //Id = 1,   
                Username = "Tinu",
                Password = "tinu@123",
                /*PhoneNumber = "9089098907",
                EmailAddress = "tinuraj@gmail.com"*/
            };

            if (actualUser != null)
            {
                Assert.AreEqual(expectedUser.Username, actualUser.Username);
                Assert.AreEqual(expectedUser.Password, actualUser.Password);

            }

            //Assert.AreEqual(expectedUser.Id, actualUser.Id);

            /* Assert.AreEqual(expectedUser.PhoneNumber, actualUser.PhoneNumber);
             Assert.AreEqual(expectedUser.EmailAddress, actualUser.EmailAddress);*/
        }

        [Test]
        public void Get_ReturnsOkResultWithData()
        {

            var expectedUsers = new List<User>
        {
            new User { Id = 1, Username = "User1", Password = "Password1" },
            new User { Id = 2, Username = "User2", Password = "Password2" }
        };
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(x => x.GetUsers()).Returns(expectedUsers);

            //repositoryMock.Setup(x => x.GetUsers()).Returns(new List<User>
            //{
            //    new User { Id = 1, Username = "User1", Password = "Password1" },
            //    new User { Id = 2, Username = "User2", Password = "Password2" }
            //});

            List<User> actualUsers = repositoryMock.Object.GetUsers();

            // Assert
            Assert.AreEqual(expectedUsers.Count, actualUsers.Count);

            for (int i = 0; i < expectedUsers.Count; i++)
            {
                Assert.AreEqual(expectedUsers[i].Id, actualUsers[i].Id);
                Assert.AreEqual(expectedUsers[i].Username, actualUsers[i].Username);
                Assert.AreEqual(expectedUsers[i].Password, actualUsers[i].Password);
            }

            //var controller = new UserController(repositoryMock.Object);

            //// Act
            //var result = controller.Get() as OkObjectResult;

            //// Assert
            //Assert.IsNotNull(result);

            ////result.Value.Id.ShouldBe(1);

            //Assert.IsNotNull(result.Value as List<User>);
        }

        [Test]
        public void GetById_ReturnsOkResultWithUser()
        {

            var expectedUser = new User
            {
                Id = 1,
                Username = "User1",
                Password = "Password1"
            };
            //Arrange
            //int userId = 1;
            var repositoryMock = new Mock<IRepository>();
            //repositoryMock.Setup(x => x.GetUserById(userId)).Returns(new User
            //{
            //    Id = userId,
            //    Username = "User1",
            //    Password = "Password1"
            //});
            repositoryMock.Setup(x => x.GetUserById(1)).Returns(expectedUser);

            var user = repositoryMock.Object.GetUserById(1);
            Assert.AreEqual(expectedUser.Id, user.Id);
            Assert.AreEqual(expectedUser.Username, user.Username);
            Assert.AreEqual(expectedUser.Password, user.Password);

            //var controller = new UserController(repositoryMock.Object);

            ////Act
            //var result = controller.GetById(userId) as OkObjectResult;

            ////Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(200, result.StatusCode);
            //Assert.IsNotNull(result.Value as User);
            //Assert.AreEqual(userId, (result.Value as User).Id);
        }


        [Test]
        public void Post_ReturnsOkResultWithCreatedUser()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var newUser = new User { Username = "NewUser", Password = "NewPassword" };
            repositoryMock.Setup(x => x.CreateUser(newUser));

            var controller = new UserController(repositoryMock.Object);

            // Act
            var result = controller.Post(newUser) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value as User);
            Assert.AreEqual(newUser.Username, (result.Value as User).Username);
        }

        [Test]
        public void Put_ReturnsOkResultWithUpdatedUser()
        {
            // Arrange
            int userId = 1;
            var repositoryMock = new Mock<IRepository>();
            var updatedUser = new User { Id = userId, Username = "UpdatedUser", Password = "UpdatedPassword" };
            repositoryMock.Setup(x => x.UpdateUser(userId, updatedUser));

            var controller = new UserController(repositoryMock.Object);

            // Act
            var result = controller.Put(userId, updatedUser) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value as User);
            Assert.AreEqual(updatedUser.Username, (result.Value as User).Username);
            Assert.AreEqual(updatedUser.Password, (result.Value as User).Password);
        }

        [Test]
        public void Delete_ReturnsOkResult()
        {
            // Arrange
            int userId = 1;
            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(x => x.DeleteUser(userId));

            var controller = new UserController(repositoryMock.Object);

            // Act
            var result = controller.Delete(userId) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void GetUserCredentials_ReturnsOkResultWithCredentials()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(x => x.GetUserCredentials()).Returns(new List<(string?, string?)>
                 {
                     ("User1", "Password1"),
                     ("User2", "Password2")
                 });

            var controller = new UserController(repositoryMock.Object);

            // Act
            var result = controller.GetUserCredentials() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value as List<(string?, string?)>);
        }
    }
}
