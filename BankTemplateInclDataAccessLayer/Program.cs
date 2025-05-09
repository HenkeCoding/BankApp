using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Services;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BankAppDataContext>(); // Add this line

builder.Services.AddRazorPages();

builder.Services.AddTransient<DataInitializer>();

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IHandleAccountService, HandleAccountService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<IDispositionService, DispositionService>();
builder.Services.AddTransient<ICardService, CardService>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddTransient<ICreateCustomerService, CreateCustomerService>();
builder.Services.AddTransient<ICustomerBalanceService, CustomerBalanceService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddResponseCaching();


builder.Services.AddDbContext<BankAppDataContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();


// Behövs för Azure!?

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BankAppDataContext>();

    if (dbContext.Database.IsRelational())
    {
        dbContext.Database.Migrate();
    }
}



using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<DataInitializer>().SeedData();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.UseResponseCaching();
app.MapRazorPages()
   .WithStaticAssets();



app.Run();
