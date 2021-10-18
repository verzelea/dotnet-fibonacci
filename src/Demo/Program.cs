﻿using System;
using System.Diagnostics;
using System.IO;
using Demo;
using Fibonacci;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
    .Build();
    
var applicationSection = configuration.GetSection("Application");
var applicationConfig = applicationSection.Get<ApplicationConfig>();

var services = new ServiceCollection();
services.AddDbContext<FibonacciDataContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
services.AddTransient<Compute>();
services.AddLogging(configure => configure.AddConsole());

using (var serviceProvider = services.BuildServiceProvider())
{
    var logger = serviceProvider.GetService<ILogger<Compute>>();
    logger.LogInformation($"Application Name : {applicationConfig.Name}");
    logger.LogInformation($"Application Message : {applicationConfig.Message}");

    Stopwatch stopwatch = new();
    stopwatch.Start();
    var compute = serviceProvider.GetService<Compute>();
    var tasks = await compute.ExecuteAsync(args);
    foreach (var task in tasks) Console.WriteLine($"Fibo result: {task}");
    stopwatch.Stop();
    Console.WriteLine($"{stopwatch.Elapsed.Seconds} s");
}