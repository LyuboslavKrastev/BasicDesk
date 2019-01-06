using BasicDesk.App.Controllers;
using BasicDesk.App.Models.ViewModels;
using BasicDesk.Data.Models.Solution;
using BasicDesk.Tests.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace BasicDesk.Tests.SolutionsControllerTests
{
    [TestClass]
    public class DetailsTests
    {
        [TestMethod]
        public async Task Details_ReturnsCorrectViewModel()
        {
            var solutions = new[]
            {
                new Solution{Id = 1, Title = "First Solution", Content = "First Content", Author = new User()},
                new Solution{Id = 2, Title = "Second Solution", Content = "Second Content", Author = new User()},
                new Solution{Id = 3, Title = "Third Solution", Content = "Third Content", Author = new User()},
            };

            var context = TestsDbContext.GetDbContext();
            context.Solutions.AddRange(solutions);

            context.SaveChanges();

            var controller = new SolutionsController(context, TestsAutoMapper.GetMapper());

            var expectedSolution = solutions[1];

            var result = await controller.Details(expectedSolution.Id);

            var model = (result as ViewResult).ViewData.Model as SolutionDetailsViewModel;

            Assert.IsNotNull(model);
            Assert.AreEqual(model.Id, expectedSolution.Id);
            Assert.AreEqual(model.Title, expectedSolution.Title);
            Assert.AreEqual(model.Content, expectedSolution.Content);
            Assert.IsInstanceOfType(model, typeof(SolutionDetailsViewModel));
        }
    }
}
