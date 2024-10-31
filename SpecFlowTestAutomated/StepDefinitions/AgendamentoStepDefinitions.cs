using TechTalk.SpecFlow;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Garbage.Collection.API.Controllers;
using Garbage.Collection.Business.Service.Interfaces;
using Garbage.Collection.Data.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Garbage.Collection.API.ViewModels;
using System.Net;

namespace SpecFlowTestAutomated.StepDefinitions
{
    [Binding]
    public sealed class AgendamentoStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly Mock<IAgendamentoService> _mockService;
        private readonly Mock<IMapper> _mockMapper;
        private AgendamentoController _controller;
        private ActionResult<AgendamentoViewModel> _response;

        public AgendamentoStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _mockService = new Mock<IAgendamentoService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new AgendamentoController(_mockService.Object, _mockMapper.Object);
        }

        [Given(@"que existe um agendamento com o ID ""(.*)""")]
        public void GivenQueExisteUmAgendamentoComOID(int id)
        {
            var agendamento = new Agendamento { Id = id, /* Adicione outros campos necessários */ };
            _mockService.Setup(s => s.ObterAgendamentoById(id)).ReturnsAsync(agendamento);
        }

        [Given(@"que não existe agendamento com o ID ""(.*)""")]
        public void GivenQueNaoExisteAgendamentoComOID(int id)
        {
            _mockService.Setup(s => s.ObterAgendamentoById(id)).ReturnsAsync((Agendamento)null);
        }

        [When(@"o usuário requisita o agendamento pelo ID ""(.*)""")]
        public async Task WhenOUsuarioRequisitaOAgendamentoPeloID(int id)
        {
            _response = await _controller.GetById(id);
        }

        [Then(@"o resultado deve ser o agendamento correspondente")]
        public void ThenOResultadoDeveSerOAgendamentoCorrespondente()
        {
            var actionResult = _response.Result;

            // Verifica se o resultado é um OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            
            // Acessa o valor do resultado e garante que é do tipo esperado
            var agendamentoViewModel = Assert.IsType<AgendamentoViewModel>(okResult.Value);

            // Log para depuração
            Console.WriteLine($"Agendamento retornado: {Newtonsoft.Json.JsonConvert.SerializeObject(agendamentoViewModel)}");
        }

        [Then(@"o status da resposta deve ser ""(.*)""")]
        public void ThenOStatusDaRespostaDeveSer(string expectedStatus)
        {
            var actionResult = _response.Result;
            int? statusCode = (actionResult as StatusCodeResult)?.StatusCode
                              ?? (actionResult as ObjectResult)?.StatusCode;

            Assert.NotNull(statusCode); // Garante que o status code não é nulo

            string actualStatus = $"{statusCode} {((HttpStatusCode)statusCode).ToString()}";
            Assert.Equal(int.Parse(expectedStatus.Split(' ')[0]), statusCode);
        }

        [Given(@"um novo agendamento com dados válidos")]
        public void GivenUmNovoAgendamentoComDadosValidos()
        {
            var agendamentoViewModel = new AgendamentoViewModel
            {
                Id = 1,
                // Preencha outros campos necessários com dados válidos
            };

            var agendamento = new Agendamento
            {
                Id = 1,
                // Preencha outros campos com valores correspondentes
            };

            // Configuração do mapeamento
            _mockMapper.Setup(m => m.Map<Agendamento>(agendamentoViewModel)).Returns(agendamento);

            // Configuração do serviço
            _mockService.Setup(s => s.CriarAgendamento(agendamento)).ReturnsAsync(agendamento);
            _scenarioContext["agendamentoViewModel"] = agendamentoViewModel;
        }

        [When(@"o usuário cria um novo agendamento")]
        public async Task WhenOUsuarioCriaUmNovoAgendamento()
        {
            var agendamentoViewModel = (AgendamentoViewModel)_scenarioContext["agendamentoViewModel"];
            _response = await _controller.Create(agendamentoViewModel);

            // Log para verificar se o método de criação foi chamado corretamente
            Console.WriteLine($"Chamada para criar agendamento com dados: {Newtonsoft.Json.JsonConvert.SerializeObject(agendamentoViewModel)}");
        }

        [Then(@"o resultado deve ser o agendamento criado")]
        public void ThenOResultadoDeveSerOAgendamentoCriado()
        {
            var actionResult = _response.Result;

            // Verifica se o resultado é um CreatedAtActionResult
            var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult);
            

            // Verifica se o valor retornado é do tipo esperado
            var agendamentoViewModel = Assert.IsType<AgendamentoViewModel>(createdResult.Value);

            // Log para depuração
            Console.WriteLine($"Agendamento criado: {Newtonsoft.Json.JsonConvert.SerializeObject(agendamentoViewModel)}");
        }

        [Given(@"um novo agendamento com dados inválidos")]
        public void GivenUmNovoAgendamentoComDadosInvalidos()
        {
            _controller.ModelState.AddModelError("Error", "Invalid data");
        }

        [When(@"o usuário tenta criar o agendamento")]
        public async Task WhenOUsuarioTentaCriarOAgendamento()
        {
            _response = await _controller.Create(new AgendamentoViewModel());
        }

        [Then(@"o resultado deve ser uma resposta ""Bad Request""")]
        public void ThenOResultadoDeveSerUmaRespostaBadRequest()
        {
            var actionResult = _response.Result;
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Given(@"o usuário possui dados válidos para atualização")]
        public void GivenOUsuarioPossuiDadosValidosParaAtualizacao()
        {
            var agendamentoViewModel = new AgendamentoViewModel { Id = 1 };
            _mockMapper.Setup(m => m.Map<Agendamento>(agendamentoViewModel)).Returns(new Agendamento { Id = 1 });
            _mockService.Setup(s => s.AtualizarAgendamento(It.IsAny<Agendamento>())).ReturnsAsync(new Agendamento { Id = 1 });
            _scenarioContext["agendamentoViewModel"] = agendamentoViewModel;
        }

        [When(@"o usuário atualiza o agendamento com o ID ""(.*)""")]
        public async Task WhenOUsuarioAtualizaOAgendamentoComOID(int id)
        {
            var agendamentoViewModel = (AgendamentoViewModel)_scenarioContext["agendamentoViewModel"];
            _response = await _controller.Update(id, agendamentoViewModel);
        }

        [Given(@"o ID ""(.*)"" existe")]
        public void GivenOIDExiste(int id)
        {
            var agendamento = new Agendamento { Id = id };
            _mockService.Setup(s => s.AtualizarAgendamento(It.IsAny<Agendamento>())).ReturnsAsync(agendamento);
        }

        [Then(@"o resultado deve ser uma resposta ""No Content""")]
        public void ThenOResultadoDeveSerUmaRespostaNoContent()
        {
            var actionResult = _response.Result;
            Assert.IsType<NoContentResult>(actionResult);
        }

        [When(@"o usuário exclui o agendamento com o ID ""(.*)""")]
        public async Task WhenOUsuarioExcluiOAgendamentoComOID(int id)
        {
            _response = await _controller.Delete(id);
        }

        [Given(@"que existe um agendamento com o ID ""(.*)"" a ser excluído")]
        public void GivenQueExisteUmAgendamentoComOIDASerExcluido(int id)
        {
            var agendamento = new Agendamento { Id = id };
            _mockService.Setup(s => s.ObterAgendamentoById(id)).ReturnsAsync(agendamento);
        }

        [Then(@"o resultado deve ser uma resposta ""Not Found""")]
        public void ThenOResultadoDeveSerUmaRespostaNotFound()
        {
            var actionResult = _response.Result;
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
