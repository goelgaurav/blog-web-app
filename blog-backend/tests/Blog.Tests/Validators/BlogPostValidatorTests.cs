using Blog.Application.Validation;
using Blog.Domain.Entities;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Tests.Validators
{
    public class BlogPostValidatorTests
    {
        private readonly BlogPostValidator _validator = new();
        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            //Arrange
            var model = new BlogPost { Title = "" };
            
            //Act
            var result = _validator.TestValidate(model);
            
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Exceeds_MaxLength()
        {
            var model = new BlogPost { Title = new string('a', 151) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Have_Error_When_Content_Is_Empty()
        {
            var model = new BlogPost { Content = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Too_Long()
        {
            var model = new BlogPost { Description = new string('a', 1001) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Should_Not_Have_Errors_When_Valid()
        {
            var model = new BlogPost
            {
                Title = "Valid",
                Content = "Valid content",
                Description = "Optional description"
            };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

    }
}
