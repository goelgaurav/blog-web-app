using Blog.Api.Middleware;
using Blog.Application.Abstractions;
using Blog.Application.Mappings;
using Blog.Application.Services.BlogPosts;
using Blog.Application.Validation;
using Blog.Persistence.DbContexts;
using Blog.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<BlogPostValidator>();
builder.Services.AddScoped<CommentValidator>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<BlogPostProfile>());

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();



app.Run();