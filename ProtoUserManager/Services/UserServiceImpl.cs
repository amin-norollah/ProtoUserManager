using Grpc.Core;

namespace ProtoUserManager.Services
{
    public class UserServiceImpl : UserService.UserServiceBase
    {
        private static List<User> users = new List<User>();
        private static int nextId = 1;

        /// <summary>
        /// creates a new user
        /// </summary>
        public override Task<UserResponse> CreateUser(User request, ServerCallContext context)
        {
            //required fields
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.NationalId))
            {
                return Task.FromResult(new UserResponse
                {
                    Success = false,
                    Message = "Name and NationalId are required"
                });
            }

            //check if a user with the same national ID exists
            var existingUser = users.FirstOrDefault(u => u.NationalId == request.NationalId);
            if (existingUser != null)
            {
                return Task.FromResult(new UserResponse
                {
                    Success = false,
                    Message = "A user with the same NationalId exists"
                });
            }

            //create user
            request.Id = nextId++;
            users.Add(request);

            return Task.FromResult(new UserResponse
            {
                Success = true,
                Message = "User created",
                Data = request
            });
        }

        /// <summary>
        /// return an existing user
        /// </summary>
        public override Task<UserResponse> GetUser(UserIdRequest request, ServerCallContext context)
        {
            var user = users.FirstOrDefault(u => u.Id == request.Id);
            return Task.FromResult(new UserResponse { Success = true, Message = "User retrieved", Data = user });
        }

        /// <summary>
        /// updates an existing user
        /// </summary>
        public override Task<UserResponse> UpdateUser(User request, ServerCallContext context)
        {
            var user = users.FirstOrDefault(u => u.Id == request.Id);
            if (user == null)
            {
                return Task.FromResult(new UserResponse { Success = false, Message = "User not found" });
            }

            user.Name = request.Name;
            user.Family = request.Family;
            user.NationalId = request.NationalId;
            user.DateOfBirth = request.DateOfBirth;

            return Task.FromResult(new UserResponse { Success = true, Message = "User updated" });
        }

        /// <summary>
        /// remove user by his ID
        /// </summary>
        public override Task<UserResponse> DeleteUser(UserIdRequest request, ServerCallContext context)
        {
            var user = users.FirstOrDefault(u => u.Id == request.Id);
            if (user == null)
            {
                return Task.FromResult(new UserResponse { Success = false, Message = "User not found" });
            }

            users.Remove(user);
            return Task.FromResult(new UserResponse { Success = true, Message = "User deleted" });
        }
    }
}