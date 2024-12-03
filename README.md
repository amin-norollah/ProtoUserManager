# ProtoUserManager: User Management Service
The User Management Service is a gRPC application that helps manage user data efficiently.
It allows CRUD operation on the user information, including name, family, national id, and date of birth. Using protobuff for data formatting, the service make communication possible between clients and server.

## How to Build
 - clone the repository
 - ```dotnet restore```
 - ```dotnet build```

## Testing the Service
 - option 1; using postman: you can test the gRPC service using postman by uploading existing protobuff configuration file.
 - option 2; Unit tests: project includes unit tests that you can run to verify functionality.
   ```dotnet test```
