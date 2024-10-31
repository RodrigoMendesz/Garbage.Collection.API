using AutoMapper;
using Garbage.Collection.API.ViewModels;
using Garbage.Collection.Business.Service.Interfaces;
using Garbage.Collection.Data.Models;
using Garbage.Collection.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Garbage.Collection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoService _service;
        private readonly IMapper _mapper;
        public AgendamentoController(IAgendamentoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

       

        [HttpGet("{id}")]
        public async Task<ActionResult<AgendamentoViewModel>> GetById(int id)
        {
            try
            {
                var agendamento = await _service.ObterAgendamentoById(id);
                if (agendamento == null)
                {
                    return NotFound();
                }
                var agendamentoViewModel = _mapper.Map<AgendamentoViewModel>(agendamento);
                return Ok(agendamentoViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AgendamentoViewModel>> Create([FromBody] AgendamentoViewModel agendamentoViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var agendamento = _mapper.Map<Agendamento>(agendamentoViewModel);
                var novoAgendamento = await _service.CriarAgendamento(agendamento);

                return CreatedAtAction(nameof(GetById), new { id = novoAgendamento.Id }, _mapper.Map<AgendamentoViewModel>(novoAgendamento));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error" );
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] AgendamentoViewModel agendamentoViewModel)
        {
            try
            {
                if (id != agendamentoViewModel.Id)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var agendamento = _mapper.Map<Agendamento>(agendamentoViewModel);
                var agendamentoAtualizado = await _service.AtualizarAgendamento(agendamento);

                if (agendamentoAtualizado == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int  id)
        {
            try
            {
                var agendamento = await _service.ObterAgendamentoById(id);
                if (agendamento == null)
                {
                    return NotFound();
                }

                await _service.ExcluirAgendamento(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgendamentoViewModel>>> Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var agendamentos = await _service.ObterAgendamentos(pageNumber, pageSize);
                if (agendamentos == null)
                {
                    return BadRequest();
                }

                return Ok(agendamentos);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }

}
