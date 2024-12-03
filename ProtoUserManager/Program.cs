using ProtoUserManager.Services;

//////////////////////////
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();

//////////////////////////
var app = builder.Build();
app.MapGrpcService<UserServiceImpl>();
app.MapGet("/", () => "Communication with gRPC endpoints");

app.Run();
