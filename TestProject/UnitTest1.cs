using System;
using System.Reflection;
using NUnit.Framework;
using dotnetapp.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class LibraryTests
    {
        private Type _entityType;
        private DbContextOptions<LibraryDbContext> _dbContextOptions;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
                .Options;

            var dbContext = new LibraryDbContext(_dbContextOptions); // Use LibraryDbContext
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up any resources if needed
        }

        [Test]
        public void TestBook_ClassExists()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("dotnetapp");
            Type bookType = assembly.GetType("dotnetapp.Models.Book");
            Assert.NotNull(bookType, "Book class does not exist.");
        }

        [Test]
        public void TestAuthor_ClassExists()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("dotnetapp");
            Type authorType = assembly.GetType("dotnetapp.Models.Author");
            Assert.NotNull(authorType, "Author class does not exist.");
        }

        [Test]
        public void TestGenre_ClassExists()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("dotnetapp");
            Type genreType = assembly.GetType("dotnetapp.Models.Genre");
            Assert.NotNull(genreType, "Genre class does not exist.");
        }

        [Test]
        public void TestMember_ClassExists()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("dotnetapp");
            Type memberType = assembly.GetType("dotnetapp.Models.Member");
            Assert.NotNull(memberType, "Member class does not exist.");
        }

        [Test]
        public void TestLoan_ClassExists()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("dotnetapp");
            Type loanType = assembly.GetType("dotnetapp.Models.Loan");
            Assert.NotNull(loanType, "Loan class does not exist.");
        }

        [Test]
        public void TestLibraryDbContext_ClassExists()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("dotnetapp");
            Type dbContextType = assembly.GetType("dotnetapp.Models.LibraryDbContext");
            Assert.NotNull(dbContextType, "LibraryDbContext class does not exist.");
        }

        [Test]
        public void TestLoanDatePropertyType_Loan_Table()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _entityType = assembly.GetType("dotnetapp.Models.Loan");
            PropertyInfo loanDateProperty = _entityType.GetProperty("LoanDate");
            Assert.NotNull(loanDateProperty, "Loan LoanDate property does not exist.");
            Assert.AreEqual(typeof(DateTime), loanDateProperty.PropertyType, "Loan LoanDate property should be of type DateTime.");
        }

        [Test]
        public void TestPublicationDatePropertyType_Book_Table()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _entityType = assembly.GetType("dotnetapp.Models.Book");
            PropertyInfo publicationDateProperty = _entityType.GetProperty("PublicationDate");
            Assert.NotNull(publicationDateProperty, "Book PublicationDate property does not exist.");
            Assert.AreEqual(typeof(DateTime), publicationDateProperty.PropertyType, "Book PublicationDate property should be of type DateTime.");
        }

        [Test]
        public void TestMigrationExists()
        {
            bool migrationsFolderExists = Directory.Exists(@"/home/coder/project/workspace/dotnetapp/Migrations");
            Assert.IsTrue(migrationsFolderExists, "Migrations folder does not exist.");
        }

        [Test]
        public void LibraryDbContext_ContainsDbSet_Book()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(LibraryDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type bookType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Book");
            if (bookType == null)
            {
                Assert.Fail("Book class not found");
                return;
            }
            var propertyInfo = contextType.GetProperty("Books");
            if (propertyInfo == null)
            {
                Assert.Fail("Books DbSet property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(bookType), propertyInfo.PropertyType);
            }
        }

        [Test]
        public void LibraryDbContext_ContainsDbSet_Loan()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(LibraryDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type loanType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Loan");
            if (loanType == null)
            {
                Assert.Fail("Loan class not found");
                return;
            }
            var propertyInfo = contextType.GetProperty("Loans");
            if (propertyInfo == null)
            {
                Assert.Fail("Loans DbSet property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(loanType), propertyInfo.PropertyType);
            }
        }
    }
}
