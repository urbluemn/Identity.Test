

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
//builder.Services.AddIdentityServer()
//    .AddInMemoryApiResources(new List<ApiResource>())
//    .AddInMemoryApiScopes(new List<ApiScope>())
//    .AddInMemoryClients(new List<Client>())
//    .AddDeveloperSigningCredential();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseRouting();
//app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
