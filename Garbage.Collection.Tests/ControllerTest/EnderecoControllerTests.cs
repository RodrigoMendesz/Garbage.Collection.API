using AutoMapper;
using Garbage.Collection.API.Controllers;
using Garbage.Collection.API.ViewModels;
using Garbage.Collection.Business.Service.Interfaces;
using Garbage.Collection.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garbage.Collection.Tests.ControllerTest
{
    public class EnderecoControllerTests
    {
        private readonly Mock<IEnderecoService> _serviceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly EnderecoController _controller;

        public EnderecoControllerTests()
        {
            _serviceMock = new Mock<IEnderecoService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new EnderecoController(_serviceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithListOfEnderecoViewModel()
        {
            // Arrange
            var enderecos = new List<Endereco> { new Endereco { Id = 1 }, new Endereco { Id = 2 } };
            var enderecoViewModels = new List<EnderecoViewModel> { new EnderecoViewModel { Id = 1 }, new EnderecoViewModel { Id = 2 } };

            _serviceMock.Setup(service => service.ObterEnderecos()).ReturnsAsync(enderecos);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<EnderecoViewModel>>(enderecos)).Returns(enderecoViewModels);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<EnderecoViewModel>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenNoEnderecosExist()
        {
            // Arrange
            _serviceMock.Setup(service => service.ObterEnderecos()).ReturnsAsync((List<Endereco>)null);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Get_ReturnsInternalServerError_OnException()
        {
            // Arrange
            _serviceMock.Setup(service => service.ObterEnderecos()).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.Get();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithEnderecoViewModel()
        {
            // Arrange
            var endereco = new Endereco { Id = 1 };
            var enderecoViewModel = new EnderecoViewModel { Id = 1 };

            _serviceMock.Setup(service => service.ObterEnderecoById(1)).ReturnsAsync(endereco);
            _mapperMock.Setup(mapper => mapper.Map<EnderecoViewModel>(endereco)).Returns(enderecoViewModel);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<EnderecoViewModel>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetById_ReturnsBadRequest_WhenEnderecoDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(service => service.ObterEnderecoById(1)).ReturnsAsync((Endereco)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsInternalServerError_OnException()
        {
            // Arrange
            _serviceMock.Setup(service => service.ObterEnderecoById(1)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.Get(1);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        

        [Fact]
        public async Task Post_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var enderecoViewModel = new EnderecoViewModel { Id = 1 };
            _mapperMock.Setup(mapper => mapper.Map<Endereco>(enderecoViewModel)).Throws(new Exception());

            // Act
            var result = await _controller.Post(enderecoViewModel);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        

        
        [Fact]
        public async Task Delete_ReturnsNoContent_OnSuccess()
        {
            // Arrange
            var endereco = new Endereco { Id = 1 };

            _serviceMock.Setup(service => service.ExcluirEndereco(1)).ReturnsAsync(endereco);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequest_WhenEnderecoDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(service => service.ExcluirEndereco(1)).ReturnsAsync((Endereco)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsInternalServerError_OnException()
        {
            // Arrange
            _serviceMock.Setup(service => service.ExcluirEndereco(1)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


    }
}