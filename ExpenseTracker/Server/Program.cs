﻿using Microsoft.EntityFrameworkCore;
using Server.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ExpenseTrackerServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExpenseTrackerServerContext") ??
                         throw new InvalidOperationException(
                             "Connection string 'ExpenseTrackerServerContext' not found.")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseWebAssemblyDebugging();
else
    app.UseHsts();

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();