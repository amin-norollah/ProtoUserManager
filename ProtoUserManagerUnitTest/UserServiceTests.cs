using ProtoUserManager.Services;

namespace ProtoUserManager.Tests
{
    public class UserServiceTests
    {
        private readonly UserServiceImpl _userService;

        public UserServiceTests()
        {
            _userService = new UserServiceImpl();
        }

        [Fact]
        public async Task ShouldAddUser()
        {
            //create user
            var createdUser = await CreateTestUser("Amin", "No", "123456789", "1992-01-01");

            //asserts
            Assert.True(createdUser.Success);
            Assert.Equal("User created", createdUser.Message);
        }

        [Fact]
        public async Task ShouldGetUser()
        {
            //create user
             var createdUser = await CreateTestUser("Amin", "No", "123456789", "1992-01-01");

            //get existing user
            var request = new UserIdRequest { Id = createdUser.Data.Id };
            var result = await _userService.GetUser(request, new TestServerCallContext());

            //asserts
            Assert.NotNull(result);
            Assert.Equal("User retrieved", result.Message);
            //Assert.Equal("Amin", result.Name);
        }

        [Fact]
        public async Task ShouldUpdateUser()
        {
            //create user
             var createdUser = await CreateTestUser("Amin", "No", "123456789", "1992-01-01");

            //update user
            var updatedUser = new User { Id = createdUser.Data.Id, Name = "Ali" };
            var response = await _userService.UpdateUser(updatedUser, new TestServerCallContext());

            //asserts
            Assert.True(response.Success);
            Assert.Equal("User updated", response.Message);

            //verify update
            var retrievedUser = await _userService.GetUser(new UserIdRequest { Id = createdUser.Data.Id }, new TestServerCallContext());
            Assert.Equal("Ali", retrievedUser.Data.Name);
        }

        [Fact]
        public async Task ShouldRemoveUser()
        {
            //create user
             var createdUser = await CreateTestUser("Amin", "No", "123456789", "1992-01-01");

            //remove user
            var request = new UserIdRequest { Id = createdUser.Data.Id };
            var response = await _userService.DeleteUser(request, new TestServerCallContext());

            //asserts
            Assert.True(response.Success);
            Assert.Equal("User deleted", response.Message);
        }

        /// <summary>
        /// method for creating user
        /// </summary>
        private async Task<UserResponse> CreateTestUser(string name, string family, string nationalId, string dateOfBirth)
        {
            var user = new User
            {
                Name = name,
                Family = family,
                NationalId = nationalId,
                DateOfBirth = dateOfBirth
            };
            return await _userService.CreateUser(user, new TestServerCallContext());
        }
    }
}
