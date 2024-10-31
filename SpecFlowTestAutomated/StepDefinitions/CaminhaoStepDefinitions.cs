using AutoMapper;
using Garbage.Collection.API.Controllers;
using Garbage.Collection.API.ViewModels;
using Garbage.Collection.Business.Service.Interfaces;
using Garbage.Collection.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

[Binding]
public class CaminhaoControllerStepDefinitions
{
    private readonly Mock<ICaminhaoService> _mockService;
    private readonly Mock<IMapper> _mockMapper;
    private ActionResult<CaminhaoViewModel> _response;

    public CaminhaoControllerStepDefinitions()
    {
        _mockService = new Mock<ICaminhaoService>();
        _mockMapper = new Mock<IMapper>();
    }

    [Given(@"que existe um caminhão com o ID ""(.*)""")]
    public async Task GivenQueExisteUmCaminhaoComOID(int id)
    {
        var caminhao = new Caminhao { Id = id };
        _mockService.Setup(s => s.ObterCaminhaoyId(id)).ReturnsAsync(caminhao);
        // Configure o Mapper
        var viewModel = new CaminhaoViewModel { Id = id };
        _mockMapper.Setup(m => m.Map<CaminhaoViewModel>(caminhao)).Returns(viewModel);
    }

    [Given(@"que não existe caminhão com o ID ""(.*)""")]
    public async Task GivenQueNaoExisteCaminhaoComOID(int id)
    {
        _mockService.Setup(s => s.ObterCaminhaoyId(id)).ReturnsAsync((Caminhao)null);
    }

    [When(@"o usuário requisita o caminhão pelo ID ""(.*)""")]
    public async Task WhenOUsuarioRequisitaOCaminhaoPeloID(int id)
    {
        var controller = new CaminhaoController(_mockService.Object, _mockMapper.Object);
        _response = await controller.Get(id);
    }

    [Then(@"o resultado deve ser o caminhão correspondente")]
    public void ThenOResultadoDeveSerOCaminhaoCorrespondente()
    {
        var okResult = Assert.IsType<OkObjectResult>(_response);
        Assert.IsType<CaminhaoViewModel>(okResult.Value);
    }

    [Then(@"o resultado deve ser uma resposta ""Not Found""")]
    public void ThenOResultadoDeveSerUmaRespostaNotFound()
    {
        Assert.IsType<NotFoundResult>(_response);
    }

    [Given(@"um novo caminhão com dados válidos")]
    public void GivenUmNovoCaminhaoComDadosValidos()
    {
        var viewModel = new CaminhaoViewModel { /* Propriedades válidas */ };
        _mockMapper.Setup(m => m.Map<Caminhao>(viewModel)).Returns(new Caminhao { /* Propriedades válidas */ });
        _mockService.Setup(s => s.CriarCaminhao(It.IsAny<Caminhao>()));
    }

    [When(@"o usuário cria um novo caminhão")]
    public async Task WhenOUsuarioCriaUmNovoCaminhao()
    {
        var controller = new CaminhaoController(_mockService.Object, _mockMapper.Object);
        var viewModel = new CaminhaoViewModel { /* Propriedades válidas */ };
        _response = await controller.Post(viewModel);
    }

    [Then(@"o resultado deve ser o caminhão criado")]
    public void ThenOResultadoDeveSerOCaminhaoCriado()
    {
        var createdResult = Assert.IsType<CreatedAtActionResult>(_response);
        Assert.NotNull(createdResult.Value);
    }

    [Given(@"um novo caminhão com dados inválidos")]
    public void GivenUmNovoCaminhaoComDadosInvalidos()
    {
        // Configure mocks para dados inválidos, se necessário
    }

    [Then(@"o resultado deve ser uma resposta ""Bad Request""")]
    public void ThenOResultadoDeveSerUmaRespostaBadRequest()
    {
        Assert.IsType<BadRequestResult>(_response);
    }

    [Given(@"que existe um caminhão com o ID ""(.*)""")]
    public async Task GivenQueExisteUmCaminhaoComOIDParaAtualizar(int id)
    {
        var caminhao = new Caminhao { Id = id };
        _mockService.Setup(s => s.AtualizarCaminhao(It.IsAny<Caminhao>())).ReturnsAsync(caminhao);
    }

    [When(@"o usuário atualiza o caminhão com o ID ""(.*)""")]
    public async Task WhenOUsuarioAtualizaOCaminhaoComOID(int id)
    {
        var controller = new CaminhaoController(_mockService.Object, _mockMapper.Object);
        var viewModel = new CaminhaoUpdateViewModel { Id = id, /* Outras propriedades válidas */ };
        _response = await controller.Put(id, viewModel);
    }

    [Then(@"o resultado deve ser uma resposta ""No Content""")]
    public void ThenOResultadoDeveSerUmaRespostaNoContent()
    {
        Assert.IsType<NoContentResult>(_response);
    }

    [Given(@"que existe um caminhão com o ID ""(.*)""")]
    public async Task GivenQueExisteUmCaminhaoComOIDParaExcluir(int id)
    {
        var caminhao = new Caminhao { Id = id };
        _mockService.Setup(s => s.ExcluirCaminhao(id)).ReturnsAsync(caminhao);
    }

    [When(@"o usuário exclui o caminhão com o ID ""(.*)""")]
    public async Task WhenOUsuarioExcluiOCaminhaoComOID(int id)
    {
        var controller = new CaminhaoController(_mockService.Object, _mockMapper.Object);
        _response = await controller.Delete(id);
    }

    [Then(@"o resultado deve ser uma resposta ""No Content""")]
    public void ThenOResultadoDeveSerUmaRespostaNoContentParaExcluir()
    {
        Assert.IsType<NoContentResult>(_response);
    }

    [When(@"o usuário tenta excluir o caminhão com o ID ""(.*)""")]
    public async Task WhenOUsuarioTentaExcluirOCaminhaoComOID(int id)
    {
        _mockService.Setup(s => s.ExcluirCaminhao(id)).ReturnsAsync((Caminhao)null);
        var controller = new CaminhaoController(_mockService.Object, _mockMapper.Object);
        _response = await controller.Delete(id);
    }
}
