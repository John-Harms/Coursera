var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/downloads/hi", () => "Hello Downloads!");
app.MapPut("/", () => "This is a put");
app.Run();
