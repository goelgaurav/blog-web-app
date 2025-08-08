using Blog.Api.Middleware;
using Blog.Application.Abstractions;
using Blog.Application.Mappings;
using Blog.Application.Services.BlogPosts;
using Blog.Application.Services.Comments;
using Blog.Application.Validation;
using Blog.Persistence.DbContexts;
using Blog.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<BlogPostValidator>();
builder.Services.AddScoped<CommentValidator>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<BlogPostProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<CommentProfile>());
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEndDev", policy =>
    {
        policy.WithOrigins("http://localhost:5174")
        .AllowAnyHeader()
        .AllowAnyMethod();

    });
});

var app = builder.Build();
app.UseCors("AllowFrontEndDev");

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.MapControllers();
app.Run();