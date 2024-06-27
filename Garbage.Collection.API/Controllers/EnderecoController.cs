using AutoMapper;
using Garbage.Collection.API.ViewModels;
using Garbage.Collection.Business.Service.Interfaces;
using Garbage.Collection.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace Garbage.Collection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _service;
        private readonly IMapper _mapper;
        public EnderecoController(IEnderecoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Endereco>>> Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var enderecos = await _service.ObterEnderecos(pageNumber, pageSize);
                if (enderecos == null)
                {
                    return BadRequest();
                }
                return Ok(enderecos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }

        }


    }
}
