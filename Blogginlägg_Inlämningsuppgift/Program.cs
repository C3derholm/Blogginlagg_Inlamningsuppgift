using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Blogginlägg_Inlämningsuppgift.Core.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();

var connString = "Data Source=SHARKPC\\SQLEXPRESS; Initial Catalog=BlogDB;Integrated Security=SSPI;TrustServerCertificate=True;";

builder.Services.AddDbContext<Blogginlägg_Inlämningsuppgift.Data.BlogDbContext>(options =>
    options.UseSqlServer(connString)
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseEndpoints(endpoints =>{endpoints.MapControllers();});

app.Run();
