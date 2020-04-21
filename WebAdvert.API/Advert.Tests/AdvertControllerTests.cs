using AdvertAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAdvert.API.Controllers;
using WebAdvert.API.Services;
using Xunit;

namespace Advert.Tests
{
    public class AdvertControllerTests
    {
        private readonly AdvertController _controller;
        private readonly Mock<IAdvertStorageService> _service;
        public AdvertControllerTests()
        {
            _service = new Mock<IAdvertStorageService>();
            _controller = new AdvertController(_service.Object);
        }

        [Fact]
        public async Task AddAdvert_Returns_200()
        {
            AdvertModel model = new AdvertModel()
            {
                Description = "TestDesc",
                Title = "TestTitle",
                Price = 200
            };
            _service.Setup(x => x.Add(It.IsAny<AdvertModel>())).Returns(Task.FromResult("Success"));
            var result = await _controller.AddAdvert(model);
            var actionResult = result as ObjectResult;
            Assert.Equal(200, actionResult.StatusCode);
        }
    }
}