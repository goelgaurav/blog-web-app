using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlogPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Author", "Content", "CreatedAt", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("01bf6d3d-1a41-43ff-972c-7adc925fbca7"), "Author 10", "This is the content for post 10.", new DateTime(2025, 7, 28, 6, 9, 0, 0, DateTimeKind.Utc), "Description for post 10", "Seed Post 10" },
                    { new Guid("077e988e-61d1-49e4-bf5f-0a182dfa05db"), "Author 6", "This is the content for post 6.", new DateTime(2025, 7, 28, 6, 5, 0, 0, DateTimeKind.Utc), "Description for post 6", "Seed Post 6" },
                    { new Guid("0ff63aa1-a57f-4df6-99b4-5902229bdfdf"), "Author 9", "This is the content for post 9.", new DateTime(2025, 7, 28, 6, 8, 0, 0, DateTimeKind.Utc), "Description for post 9", "Seed Post 9" },
                    { new Guid("1e2ebd66-7ef8-4898-990d-cab47e92b070"), "Author 2", "This is the content for post 2.", new DateTime(2025, 7, 28, 6, 1, 0, 0, DateTimeKind.Utc), "Description for post 2", "Seed Post 2" },
                    { new Guid("36d0139d-0163-492a-823f-74dc5aebcbe3"), "Author 5", "This is the content for post 5.", new DateTime(2025, 7, 28, 6, 4, 0, 0, DateTimeKind.Utc), "Description for post 5", "Seed Post 5" },
                    { new Guid("c7d08df2-3835-45ec-8e10-7952543e4f35"), "Author 8", "This is the content for post 8.", new DateTime(2025, 7, 28, 6, 7, 0, 0, DateTimeKind.Utc), "Description for post 8", "Seed Post 8" },
                    { new Guid("d31ad2dd-6bd8-40cf-a105-6bb2837f2262"), "Author 3", "This is the content for post 3.", new DateTime(2025, 7, 28, 6, 2, 0, 0, DateTimeKind.Utc), "Description for post 3", "Seed Post 3" },
                    { new Guid("eb21c477-f44b-4b4d-8309-c46eb88a8e5e"), "Author 1", "This is the content for post 1.", new DateTime(2025, 7, 28, 6, 0, 0, 0, DateTimeKind.Utc), "Description for post 1", "Seed Post 1" },
                    { new Guid("f6c9785d-19f0-4dba-9070-200173ba21a5"), "Author 4", "This is the content for post 4.", new DateTime(2025, 7, 28, 6, 3, 0, 0, DateTimeKind.Utc), "Description for post 4", "Seed Post 4" },
                    { new Guid("f8ff5e9d-a55b-4595-8d50-b4292cbfc6ce"), "Author 7", "This is the content for post 7.", new DateTime(2025, 7, 28, 6, 6, 0, 0, DateTimeKind.Utc), "Description for post 7", "Seed Post 7" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Author", "BlogPostId", "Content", "PostedAt" },
                values: new object[,]
                {
                    { new Guid("01010000-0000-0000-0000-000000000000"), "Commenter 1", new Guid("eb21c477-f44b-4b4d-8309-c46eb88a8e5e"), "Comment 1 for post 1", new DateTime(2025, 7, 28, 6, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("01020000-0000-0000-0000-000000000000"), "Commenter 2", new Guid("eb21c477-f44b-4b4d-8309-c46eb88a8e5e"), "Comment 2 for post 1", new DateTime(2025, 7, 28, 6, 1, 0, 0, DateTimeKind.Utc) },
                    { new Guid("01030000-0000-0000-0000-000000000000"), "Commenter 3", new Guid("eb21c477-f44b-4b4d-8309-c46eb88a8e5e"), "Comment 3 for post 1", new DateTime(2025, 7, 28, 6, 2, 0, 0, DateTimeKind.Utc) },
                    { new Guid("02010000-0000-0000-0000-000000000000"), "Commenter 1", new Guid("1e2ebd66-7ef8-4898-990d-cab47e92b070"), "Comment 1 for post 2", new DateTime(2025, 7, 28, 6, 3, 0, 0, DateTimeKind.Utc) },
                    { new Guid("02020000-0000-0000-0000-000000000000"), "Commenter 2", new Guid("1e2ebd66-7ef8-4898-990d-cab47e92b070"), "Comment 2 for post 2", new DateTime(2025, 7, 28, 6, 4, 0, 0, DateTimeKind.Utc) },
                    { new Guid("02030000-0000-0000-0000-000000000000"), "Commenter 3", new Guid("1e2ebd66-7ef8-4898-990d-cab47e92b070"), "Comment 3 for post 2", new DateTime(2025, 7, 28, 6, 5, 0, 0, DateTimeKind.Utc) },
                    { new Guid("03010000-0000-0000-0000-000000000000"), "Commenter 1", new Guid("d31ad2dd-6bd8-40cf-a105-6bb2837f2262"), "Comment 1 for post 3", new DateTime(2025, 7, 28, 6, 6, 0, 0, DateTimeKind.Utc) },
                    { new Guid("03020000-0000-0000-0000-000000000000"), "Commenter 2", new Guid("d31ad2dd-6bd8-40cf-a105-6bb2837f2262"), "Comment 2 for post 3", new DateTime(2025, 7, 28, 6, 7, 0, 0, DateTimeKind.Utc) },
                    { new Guid("03030000-0000-0000-0000-000000000000"), "Commenter 3", new Guid("d31ad2dd-6bd8-40cf-a105-6bb2837f2262"), "Comment 3 for post 3", new DateTime(2025, 7, 28, 6, 8, 0, 0, DateTimeKind.Utc) },
                    { new Guid("04010000-0000-0000-0000-000000000000"), "Commenter 1", new Guid("f6c9785d-19f0-4dba-9070-200173ba21a5"), "Comment 1 for post 4", new DateTime(2025, 7, 28, 6, 9, 0, 0, DateTimeKind.Utc) },
                    { new Guid("04020000-0000-0000-0000-000000000000"), "Commenter 2", new Guid("f6c9785d-19f0-4dba-9070-200173ba21a5"), "Comment 2 for post 4", new DateTime(2025, 7, 28, 6, 10, 0, 0, DateTimeKind.Utc) },
                    { new Guid("04030000-0000-0000-0000-000000000000"), "Commenter 3", new Guid("f6c9785d-19f0-4dba-9070-200173ba21a5"), "Comment 3 for post 4", new DateTime(2025, 7, 28, 6, 11, 0, 0, DateTimeKind.Utc) },
                    { new Guid("05010000-0000-0000-0000-000000000000"), "Commenter 1", new Guid("36d0139d-0163-492a-823f-74dc5aebcbe3"), "Comment 1 for post 5", new DateTime(2025, 7, 28, 6, 12, 0, 0, DateTimeKind.Utc) },
                    { new Guid("05020000-0000-0000-0000-000000000000"), "Commenter 2", new Guid("36d0139d-0163-492a-823f-74dc5aebcbe3"), "Comment 2 for post 5", new DateTime(2025, 7, 28, 6, 13, 0, 0, DateTimeKind.Utc) },
                    { new Guid("05030000-0000-0000-0000-000000000000"), "Commenter 3", new Guid("36d0139d-0163-492a-823f-74dc5aebcbe3"), "Comment 3 for post 5", new DateTime(2025, 7, 28, 6, 14, 0, 0, DateTimeKind.Utc) },
                    { new Guid("06010000-0000-0000-0000-000000000000"), "Commenter 1", new Guid("077e988e-61d1-49e4-bf5f-0a182dfa05db"), "Comment 1 for post 6", new DateTime(2025, 7, 28, 6, 15, 0, 0, DateTimeKind.Utc) },
                    { new Guid("06020000-0000-0000-0000-000000000000"), "Commenter 2", new Guid("077e988e-61d1-49e4-bf5f-0a182dfa05db"), "Comment 2 for post 6", new DateTime(2025, 7, 28, 6, 16, 0, 0, DateTimeKind.Utc) },
                    { new Guid("06030000-0000-0000-0000-000000000000"), "Commenter 3", new Guid("077e988e-61d1-49e4-bf5f-0a182dfa05db"), "Comment 3 for post 6", new DateTime(2025, 7, 28, 6, 17, 0, 0, DateTimeKind.Utc) },
                    { new Guid("07010000-0000-0000-0000-000000000000"), "Commenter 1", new Guid("f8ff5e9d-a55b-4595-8d50-b4292cbfc6ce"), "Comment 1 for post 7", new DateTime(2025, 7, 28, 6, 18, 0, 0, DateTimeKind.Utc) },
                    { new Guid("07020000-0000-0000-0000-000000000000"), "Commenter 2", new Guid("f8ff5e9d-a55b-4595-8d50-b4292cbfc6ce"), "Comment 2 for post 7", new DateTime(2025, 7, 28, 6, 19, 0, 0, DateTimeKind.Utc) },
                    { new Guid("07030000-0000-0000-0000-000000000000"), "Commenter 3", new Guid("f8ff5e9d-a55b-4595-8d50-b4292cbfc6ce"), "Comment 3 for post 7", new DateTime(2025, 7, 28, 6, 20, 0, 0, DateTimeKind.Utc) },
                    { new Guid("08010000-0000-0000-0000-000000000000"), "Commenter 1", new Guid("c7d08df2-3835-45ec-8e10-7952543e4f35"), "Comment 1 for post 8", new DateTime(2025, 7, 28, 6, 21, 0, 0, DateTimeKind.Utc) },
                    { new Guid("08020000-0000-0000-0000-000000000000"), "Commenter 2", new Guid("c7d08df2-3835-45ec-8e10-7952543e4f35"), "Comment 2 for post 8", new DateTime(2025, 7, 28, 6, 22, 0, 0, DateTimeKind.Utc) },
                    { new Guid("08030000-0000-0000-0000-000000000000"), "Commenter 3", new Guid("c7d08df2-3835-45ec-8e10-7952543e4f35"), "Comment 3 for post 8", new DateTime(2025, 7, 28, 6, 23, 0, 0, DateTimeKind.Utc) },
                    { new Guid("09010000-0000-0000-0000-000000000000"), "Commenter 1", new Guid("0ff63aa1-a57f-4df6-99b4-5902229bdfdf"), "Comment 1 for post 9", new DateTime(2025, 7, 28, 6, 24, 0, 0, DateTimeKind.Utc) },
                    { new Guid("09020000-0000-0000-0000-000000000000"), "Commenter 2", new Guid("0ff63aa1-a57f-4df6-99b4-5902229bdfdf"), "Comment 2 for post 9", new DateTime(2025, 7, 28, 6, 25, 0, 0, DateTimeKind.Utc) },
                    { new Guid("09030000-0000-0000-0000-000000000000"), "Commenter 3", new Guid("0ff63aa1-a57f-4df6-99b4-5902229bdfdf"), "Comment 3 for post 9", new DateTime(2025, 7, 28, 6, 26, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10010000-0000-0000-0000-000000000000"), "Commenter 1", new Guid("01bf6d3d-1a41-43ff-972c-7adc925fbca7"), "Comment 1 for post 10", new DateTime(2025, 7, 28, 6, 27, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10020000-0000-0000-0000-000000000000"), "Commenter 2", new Guid("01bf6d3d-1a41-43ff-972c-7adc925fbca7"), "Comment 2 for post 10", new DateTime(2025, 7, 28, 6, 28, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10030000-0000-0000-0000-000000000000"), "Commenter 3", new Guid("01bf6d3d-1a41-43ff-972c-7adc925fbca7"), "Comment 3 for post 10", new DateTime(2025, 7, 28, 6, 29, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogPostId",
                table: "Comments",
                column: "BlogPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "BlogPosts");
        }
    }
}
