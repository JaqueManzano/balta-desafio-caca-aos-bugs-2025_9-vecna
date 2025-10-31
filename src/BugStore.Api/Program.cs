using BugStore.Application.Interfaces;
using BugStore.Application.Mappings;
using BugStore.Application.Services;
using BugStore.Application.UseCases.Customers.Search;
using BugStore.Application.UseCases.Orders.Search;
using BugStore.Application.UseCases.Products.Search;
using BugStore.Application.UseCases.Reports.BestCustomers.Search;
using BugStore.Application.UseCases.Reports.RevenueByPeriod.Search;
using BugStore.Domain.Interfaces;
using BugStore.Handlers.Customers;
using BugStore.Infrastructure.Data;
using BugStore.Infrastructure.Repositories;
using BugStore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var envConnection = Environment.GetEnvironmentVariable("ConnectionStrings__Default");
string connectionString = builder.Configuration.GetConnectionString("Default");

if (!string.IsNullOrEmpty(envConnection))
{
    if (envConnection.StartsWith("postgresql://"))
        envConnection = envConnection.Replace("postgresql://", "postgres://");

    var databaseUri = new Uri(envConnection);
    var userInfo = databaseUri.UserInfo.Split(':');

    var npgsqlBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = databaseUri.Host,
        Port = databaseUri.Port > 0 ? databaseUri.Port : 5432,
        Username = userInfo[0],
        Password = userInfo.Length > 1 ? userInfo[1] : "",
        Database = databaseUri.AbsolutePath.TrimStart('/'),
        SslMode = SslMode.Require,
        TrustServerCertificate = true
    };

    connectionString = npgsqlBuilder.ConnectionString;
}

if (connectionString.Contains("Data Source="))
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
else
    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ProductProfle>();
    cfg.AddProfile<CustomerProfile>();
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCustomerHandler).Assembly));

builder.Services.AddScoped<IReportBestCustomerRepository, ReportBestCustomerRepository>();
builder.Services.AddScoped<IReportRevenueByPeriodRepository, ReportRevenueByPeriodRepository>();
builder.Services.AddScoped<ISearchBestCustomersUseCase, SearchBestCustomersUseCase>();
builder.Services.AddScoped<ISearchRevenueByPeriodUseCase, SearchRevenueByPeriodUseCase>();
builder.Services.AddScoped<ISearchCustomersUseCase, SearchCustomersUseCase>();
builder.Services.AddScoped<ISearchProductsUseCase, SearchProductsUseCase>();
builder.Services.AddScoped<ISearchOrdersUseCase, SearchOrdersUseCase>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //dbContext.Database.Migrate();
}

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "BugStore API v1");
    options.RoutePrefix = string.Empty;
});

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT");
if (port != null)
    app.Urls.Add($"http://0.0.0.0:{port}");
else
    app.Urls.Add("http://localhost:5000");

app.Run();
