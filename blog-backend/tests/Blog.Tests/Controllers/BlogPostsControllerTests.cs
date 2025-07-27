using Blog.Api;
using Blog.Application.DTOs;
using Blog.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Blog.Tests.Controllers;

public class BlogPostsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BlogPostsControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/BlogPosts");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Post_WithInvalidModel_ShouldReturnBadRequest()
    {
        var post = new BlogPost { Title = "" }; // Invalid input
        var response = await _client.PostAsJsonAsync("/api/BlogPosts", post);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetAll_ReturnsOkAndList()
    {
        var response = await _client.GetAsync("/api/blogposts");
        response.EnsureSuccessStatusCode();

        var posts = await response.Content.ReadFromJsonAsync<List<BlogPostResponse>>();
        Assert.NotNull(posts);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenInvalid()
    {
        var response = await _client.GetAsync($"/api/blogposts/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_CreatesBlogPost()
    {
        var request = new BlogPostRequest
        {
            Title = "Integration Test Post",
            Content = "Some test content",
            Author = "Test User",
            Description = "Test Desc"
        };

        var response = await _client.PostAsJsonAsync("/api/blogposts", request);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<BlogPostResponse>();
        Assert.Equal(request.Title, result?.Title);
    }

    [Fact]
    public async Task Put_ReturnsBadRequest_WhenInvalidId()
    {
        var update = new BlogPostRequest
        {
            Title = "Update Title",
            Content = "Update Content",
            Author = "Author",
            Description = "Update Desc"
        };

        var response = await _client.PutAsJsonAsync($"/api/blogposts/{Guid.NewGuid()}", update);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenMissing()
    {
        var response = await _client.DeleteAsync($"/api/blogposts/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
