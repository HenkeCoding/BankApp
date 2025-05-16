// See https://aka.ms/new-console-template for more information
using AntiMoneyLaunderer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


var builder = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", true, true);

var config = builder.Build();



var options = new DbContextOptionsBuilder<BankAppDataContext>();

var connectionString = config.GetConnectionString("DefaultConnection");

options.UseSqlServer(connectionString);


BankAppDataContext dbContext = new BankAppDataContext(options.Options);


MainMenu mainMenu = new MainMenu(dbContext);

mainMenu.Run();

