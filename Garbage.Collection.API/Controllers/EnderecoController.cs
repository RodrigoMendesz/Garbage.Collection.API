using AutoMapper;
using Garbage.Collection.API.ViewModels;
using Garbage.Collection.Business.Service.Interfaces;
using Garbage.Collection.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Garbage.Collection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _service;
        private readonly IMapper _mapper;
        public EnderecoController(IEnderecoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoViewModel>>> Get()
        {
            try
            {
                var endereco = await _service.ObterEnderecos();
                if (endereco == null)
                {
                    return NotFound();
                }
                var viewModelList = _mapper.Map<IEnumerable<EnderecoViewModel>>(endereco);

                return Ok(viewModelList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EnderecoViewModel>> Get(int id)
        {
            try
            {
                var endereco = await _service.ObterEnderecoById(id);
                if (endereco == null)
                {
                    return BadRequest();
                }
                var viewModelList = _mapper.Map<EnderecoViewModel>(endereco);

                return Ok(viewModelList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }

        }
        [HttpPost]
        public async Task<ActionResult<EnderecoViewModel>> Post([FromBody] EnderecoViewModel viewModel)
        {
            try
            {
                var endereco = _mapper.Map<Endereco>(viewModel);
                await _service.CriarEndereco(endereco);
                return CreatedAtAction(nameof(Get), new { id = endereco.Id }, endereco);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EnderecoViewModel>> Put(int id, EnderecoViewModel viewModel)
        {
            try
            {
                var endereco = _mapper.Map<Endereco>(viewModel);

                if (id != endereco.Id)
                {
                    return BadRequest();
                }

                var updateEndereco = await _service.AtualizarEndereco(endereco);
                if (updateEndereco == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var endereco = await _service.ExcluirEndereco(id);
                if (endereco == null)
                {
                    return BadRequest();
                }
                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
            
        }


    }
}
