using AutoMapper;
using Garbage.Collection.API.Controllers;
using Garbage.Collection.API.ViewModels;
using Garbage.Collection.Business.Service.Interfaces;
using Garbage.Collection.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechTalk.SpecFlow;

namespace SpecFlowTestAutomated.StepDefinitions
{
    [Binding]
    public class EnderecoStepDefinitions
    {
        private readonly Mock<IEnderecoService> _mockService = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private ActionResult<EnderecoViewModel> _response;

        [Given(@"um Endereco existente com o id (.*)")]
        public async Task GivenUmEnderecoExistenteComOId(int id)
        {
            // Simule o retorno do serviço para o Endereco
            var endereco = new Endereco { Id = id, /* outras propriedades */ };
            _mockService.Setup(s => s.ObterEnderecoById(id)).ReturnsAsync(endereco);

            var viewModel = new EnderecoViewModel { Id = id, /* outras propriedades */ };
            _mockMapper.Setup(m => m.Map<EnderecoViewModel>(endereco)).Returns(viewModel);
        }

        [When(@"o usuário solicita o Endereco com o id (.*)")]
        public async Task WhenOUsuarioSolicitaOEnderecoComOId(int id)
        {
            var controller = new EnderecoController(_mockService.Object, _mockMapper.Object);
            _response = await controller.Get(id);
        }

        [Then(@"o resultado deve ser o Endereco correspondente")]
        public void ThenOResultadoDeveSerOEnderecoCorrespondente()
        {
            var okResult = Assert.IsType<OkObjectResult>(_response.Result);
            Assert.IsType<EnderecoViewModel>(okResult.Value);
        }

        [When(@"o usuário cria um novo Endereco")]
        public async Task WhenOUsuarioCriaUmNovoEndereco()
        {
            var controller = new EnderecoController(_mockService.Object, _mockMapper.Object);
            var viewModel = new EnderecoViewModel { /* Propriedades válidas */ };
            _response = await controller.Post(viewModel);
        }

        [Then(@"o Endereco deve ser criado com sucesso")]
        public void ThenOEnderecoDeveSerCriadoComSucesso()
        {
            var createdResult = Assert.IsType<CreatedAtActionResult>(_response.Result);
            Assert.NotNull(createdResult);
        }

        [When(@"o usuário atualiza o Endereco com o id (.*)")]
        public async Task WhenOUsuarioAtualizaOEnderecoComOId(int id)
        {
            var controller = new EnderecoController(_mockService.Object, _mockMapper.Object);
            var viewModel = new EnderecoViewModel { Id = id, /* outras propriedades válidas */ };
            _response = await controller.Put(id, viewModel);
        }

        [Then(@"o Endereco deve ser atualizado com sucesso")]
        public void ThenOEnderecoDeveSerAtualizadoComSucesso()
        {
            Assert.IsType<NoContentResult>(_response.Result);
        }

        [When(@"o usuário solicita a exclusão do Endereco com o id (.*)")]
        public async Task WhenOUsuarioSolicitaAExclusaoDoEnderecoComOId(int id)
        {
            var controller = new EnderecoController(_mockService.Object, _mockMapper.Object);
            _response = await controller.Delete(id);
        }

        [Then(@"o Endereco deve ser excluído com sucesso")]
        public void ThenOEnderecoDeveSerExcluidoComSucesso()
        {
            Assert.IsType<NoContentResult>(_response.Result);
        }
    }
}
