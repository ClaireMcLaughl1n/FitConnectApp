using Xunit;
using FitConnectApp.Data.Entities;
using FitConnectApp.Data.Services;
using Microsoft.EntityFrameworkCore;
using FitConnectApp.Data.Repositories;
using FitConnectApp.Data.Security;

namespace FitConnectApp.Test
{
    [Collection("Sequential")]
    public class ServiceTests
    {   
        private readonly IUserService service;

        // public ServiceTests()
        // {
        //     // configure the data context options to use sqlite for testing
        //     var options = DatabaseContext.OptionsBuilder                            
        //                     .UseSqlite("Filename=test.db")
        //                     //.LogTo(Console.WriteLine)
        //                     .Options;
        //     var dbContext = new DatabaseContext(options);
        //     dbContext.Database.EnsureDeleted();
        //     dbContext.Database.EnsureCreated();
        //     service = new UserServiceDb(dbContext);
        //     service.Initialise();

        // }

        [Fact]
        public void GetUsers_WhenNoneExist_ShouldReturnNone()
        {
            // act
            var users = service.GetUsers();

            // assert
            Assert.Equal(0, users.Count);
        }
        
        [Fact]
        public void AddUser_When2ValidUsersAdded_ShouldCreate2Users()
        {
            // arrange
            service.AddUser("admin", "admin@mail.com", "admin", Role.Admin, 1.7 );
            service.AddUser("user", "user@mail.com", "user", Role.User, 1.5);

            // act
            var users = service.GetUsers();

            // assert
            Assert.Equal(2, users.Count);
        }

        [Fact]
        public void GetPage1WithpageSize2_When3UsersExist_ShouldReturn2Pages()
        {
            // act
            service.AddUser("admin", "admin@mail.com", "admin", Role.Admin, 1.7 );
            service.AddUser("userA", "userA@mail.com", "user", Role.User, 1.5);
        service.AddUser("userB", "userB@mail.com", "user", Role.User, 1.8);

            // return first page with 2 users per page
            var pagedUsers = service.GetUsers(1,2);

            // assert
            Assert.Equal(2, pagedUsers.TotalPages);
        }

        [Fact]
        public void GetPage1WithPageSize2_When3UsersExist_ShouldReturnPageWith2Users()
        {
            // act
            service.AddUser("admin", "admin@mail.com", "admin", Role.Admin, 1.7 );
            service.AddUser("userA", "userA@mail.com", "user", Role.User, 1.5);
            service.AddUser("userB", "userB@mail.com", "user", Role.User, 1.8);

            var pagedUsers = service.GetUsers(1,2);

            // assert
            Assert.Equal(2, pagedUsers.Data.Count);
        }

        [Fact]
        public void GetPage1_When0UsersExist_ShouldReturn0Pages()
        {
            // act
            var pagedUsers = service.GetUsers(1,2);

            // assert
            Assert.Equal(0, pagedUsers.TotalPages);
            Assert.Equal(0, pagedUsers.TotalRows);
            Assert.Empty(pagedUsers.Data);
        }

        [Fact]
        public void UpdateUser_WhenUserExists_ShouldWork()
        {
            // arrange
            var user = service.AddUser("admin", "admin@mail.com", "admin", Role.Admin, 1.7 );
            
            // act
            user.Name = "administrator";
            user.Email = "admin@mail.com";            
            var updatedUser = service.UpdateUser(user);

            // assert
            Assert.Equal("administrator", updatedUser.Name);
            Assert.Equal("admin@mail.com", updatedUser.Email);
        }

        [Fact]
        public void Login_WithValidCredentials_ShouldWork()
        {
            // arrange
            service.AddUser("admin", "admin@mail.com", "admin", Role.Admin, 1.7 );
            
            // act            
            var user = service.Authenticate("admin@mail.com","admin");

            // assert
            Assert.NotNull(user);
            
        }

        [Fact]
        public void Login_WithInvalidCredentials_ShouldNotWork()
        {
            // arrange
            service.AddUser("admin", "admin@mail.com", "admin", Role.Admin, 1.7 );

            // act      
            var user = service.Authenticate("admin@mail.com","xxx");

            // assert
            Assert.Null(user);
            
        }

        [Fact]
        public void ForgotPasswordRequest_ForValidUser_ShouldGenerateToken()
        {
            // arrange
            service.AddUser("admin", "admin@mail.com", "admin", Role.Admin, 1.7 );

            // act      
            var token = service.ForgotPassword("admin@mail.com");

            // assert
            Assert.NotNull(token);
            
        }

        [Fact]
        public void ForgotPasswordRequest_ForInValidUser_ShouldReturnNull()
        {
            // arrange
            
            // act      
            var token = service.ForgotPassword("admin@mail.com");

            // assert
            Assert.Null(token);
            
        }

        [Fact]
        public void ResetPasswordRequest_WithValidUserAndToken_ShouldReturnUser()
        {
            // arrange
            service.AddUser("admin", "admin@mail.com", "admin", Role.Admin, 1.7 );
            var token = service.ForgotPassword("admin@mail.com");
            
            // act      
            var user = service.ResetPassword("admin@mail.com", token, "password");
        
            // assert
            Assert.NotNull(user);
            Assert.True(Hasher.ValidateHash(user.Password, "password"));          
        }

        [Fact]
        public void ResetPasswordRequest_WithValidUserAndExpiredToken_ShouldReturnNull()
        {
            // arrange
            service.AddUser("admin", "admin@mail.com", "admin", Role.Admin, 1.7 );
            var expiredToken = service.ForgotPassword("admin@mail.com");
            service.ForgotPassword("admin@mail.com");
            
            // act      
            var user = service.ResetPassword("admin@mail.com", expiredToken, "password");
        
            // assert
            Assert.Null(user);  
        }

        [Fact]
        public void ResetPasswordRequest_WithInValidUserAndValidToken_ShouldReturnNull()
        {
            // arrange
            service.AddUser("admin", "admin@mail.com", "admin", Role.Admin, 1.7 );          
            var token = service.ForgotPassword("admin@mail.com");
            
            // act      
            var user = service.ResetPassword("unknown@mail.com", token, "password");
        
            // assert
            Assert.Null(user);  
        }

        [Fact]
        public void ResetPasswordRequests_WhenAllCompleted_ShouldExpireAllTokens()
        {
            // arrange
            service.AddUser("admin", "admin@mail.com", "admin", Role.Admin, 1.7 );       
            service.AddUser("user", "user@mail.com", "client", Role.User, 1.5);          

            // create token and reset password - token then invalidated
            var token1 = service.ForgotPassword("admin@mail.com");
            service.ResetPassword("admin@mail.com", token1, "password");

            // create token and reset password - token then invalidated
            var token2 = service.ForgotPassword("user@mail.com");
            service.ResetPassword("user@mail.com", token2, "password");
            
            // act  
            // retrieve valid tokens 
            var tokens = service.GetValidPasswordResetTokens();   

            // assert
            Assert.Empty(tokens);
        }
    }
}
  