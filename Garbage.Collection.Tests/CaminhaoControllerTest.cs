using AutoMapper;
using Garbage.Collection.API.Controllers;
using Garbage.Collection.API.ViewModels;
using Garbage.Collection.Business.Service.Interfaces;
using Garbage.Collection.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Garbage.Collection.Tests
{
    public class CaminhaoControllerTests
    {
        private readonly Mock<ICaminhaoService> _serviceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CaminhaoController _controller;

        public CaminhaoControllerTests()
        {
            _serviceMock = new Mock<ICaminhaoService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new CaminhaoController(_serviceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithListOfCaminhaoViewModel()
        {
            // Arrange
            var caminhoes = new List<Caminhao> { new Caminhao { Id = 1 }, new Caminhao { Id = 2 } };
            var caminhoesViewModel = new List<CaminhaoViewModel> { new CaminhaoViewModel { Id = 1 }, new CaminhaoViewModel { Id = 2 } };

            _serviceMock.Setup(service => service.ObterCaminhao()).ReturnsAsync(caminhoes);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<CaminhaoViewModel>>(caminhoes)).Returns(caminhoesViewModel);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<CaminhaoViewModel>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenNoCaminhoesExist()
        {
            // Arrange
            _serviceMock.Setup(service => service.ObterCaminhao()).ReturnsAsync((List<Caminhao>)null);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Get_ReturnsInternalServerError_OnException()
        {
            // Arrange
            _serviceMock.Setup(service => service.ObterCaminhao()).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.Get();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithCaminhaoViewModel()
        {
            // Arrange
            var caminhao = new Caminhao { Id = 1 };
            var caminhaoViewModel = new CaminhaoViewModel { Id = 1 };

            _serviceMock.Setup(service => service.ObterCaminhaoyId(1)).ReturnsAsync(caminhao);
            _mapperMock.Setup(mapper => mapper.Map<CaminhaoViewModel>(caminhao)).Returns(caminhaoViewModel);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<CaminhaoViewModel>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenCaminhaoDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(service => service.ObterCaminhaoyId(1)).ReturnsAsync((Caminhao)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsInternalServerError_OnException()
        {
            // Arrange
            _serviceMock.Setup(service => service.ObterCaminhaoyId(1)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.Get(1);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public void Post_ReturnsCreatedAtActionResult_WithCaminhao()
        {
            // Arrange
            var caminhaoViewModel = new CaminhaoViewModel { Id = 1 };
            var caminhao = new Caminhao { Id = 1 };

            _mapperMock.Setup(mapper => mapper.Map<Caminhao>(caminhaoViewModel)).Returns(caminhao);
            _serviceMock.Setup(service => service.CriarCaminhao(caminhao)).Verifiable();

            // Act
            var result = _controller.Post(caminhaoViewModel);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.Get), createdAtActionResult.ActionName);
            Assert.Equal(caminhao.Id, ((Caminhao)createdAtActionResult.Value).Id);
        }

        [Fact]
        public void Post_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var caminhaoViewModel = new CaminhaoViewModel { Id = 1 };
            _mapperMock.Setup(mapper => mapper.Map<Caminhao>(caminhaoViewModel)).Throws(new Exception());

            // Act
            var result = _controller.Post(caminhaoViewModel);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        } 
        // Continue writing similar tests for Post, Put, and Delete methods...
    }
}