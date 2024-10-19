using bwms_core_domain.SystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace bwms_core_unit_test
{
    public class UserTests
    {
        [Fact]
        public void User_DefaultValues_ShouldBeInitializedCorrectly()
        {
            var user = new User();

            Assert.Equal(0, user.UserId);
            Assert.Equal(string.Empty, user.UserName);
            Assert.Equal(string.Empty, user.Password);
            Assert.Equal(string.Empty, user.PasswordSalt);
            Assert.Equal(0, user.ActivationCode);
            Assert.Equal(DateTime.MinValue, user.CreatedDate);
            Assert.Equal(DateTime.MinValue, user.LastLogginDate);
            Assert.False(user.IsAuthority);
            Assert.False(user.IsActive);
            Assert.False(user.IsDeleted);
            Assert.NotEqual(Guid.Empty, user.UserGlobalIdentity);
        }

        [Fact]
        public void User_ShouldSetPropertiesCorrectly()
        {
            var user = new User
            {
                UserId = 101,
                UserName = "Alice",
                Password = "securePassword123",
                PasswordSalt = "randomSalt",
                ActivationCode = 9999,
                CreatedDate = new DateTime(2023, 1, 1),
                LastLogginDate = new DateTime(2023, 10, 20),
                IsAuthority = true,
                IsActive = true,
                IsDeleted = false,
                UserGlobalIdentity = Guid.NewGuid()
            };

            Assert.Equal(101, user.UserId);
            Assert.Equal("Alice", user.UserName);
            Assert.Equal("securePassword123", user.Password);
            Assert.Equal("randomSalt", user.PasswordSalt);
            Assert.Equal(9999, user.ActivationCode);
            Assert.Equal(new DateTime(2023, 1, 1), user.CreatedDate);
            Assert.Equal(new DateTime(2023, 10, 20), user.LastLogginDate);
            Assert.True(user.IsAuthority);
            Assert.True(user.IsActive);
            Assert.False(user.IsDeleted);
            Assert.NotEqual(Guid.Empty, user.UserGlobalIdentity);
        }

        [Fact]
        public void User_ShouldFailForInvalidActivationCode()
        {
            var user = new User
            {
                ActivationCode = -1
            };

            Assert.True(user.ActivationCode > 0);
        }

        [Fact]
        public void User_ShouldFailForInvalidActivationCodeLength()
        {
            var user = new User
            {
                ActivationCode = 12345
            };

            Assert.InRange(user.ActivationCode, 100000, 999999);
        }


        [Fact]
        public void User_ShouldHaveEmptyStringsByDefault()
        {
            var user = new User();

            Assert.Equal(string.Empty, user.UserName);
            Assert.Equal(string.Empty, user.Password);
            Assert.Equal(string.Empty, user.PasswordSalt);
        }

        [Fact]
        public void User_ShouldFailWhenLastLoginDateIsInTheFuture()
        {
            var user = new User
            {
                LastLogginDate = DateTime.Now.AddDays(1)
            };

            Assert.True(user.LastLogginDate <= DateTime.Now);
        }


    }
}
