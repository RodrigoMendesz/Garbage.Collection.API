using AutoMapper;
using Garbage.Collection.API.ViewModels;
using Garbage.Collection.Business.Service.Interfaces;
using Garbage.Collection.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Garbage.Collection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CaminhaoController : ControllerBase
    {
        private readonly ICaminhaoService _service;
        private readonly IMapper _mapper;
        public CaminhaoController(ICaminhaoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaminhaoViewModel>>> Get()
        {
            try
            {
                var caminhoes = await _service.ObterCaminhao();
                if (caminhoes == null)
                {
                    NotFound();
                }
                var viewModelList = _mapper.Map<IEnumerable<CaminhaoViewModel>>(caminhoes);
                
                return Ok(viewModelList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CaminhaoViewModel>> Get(int id)
        {
            try
            {
                var caminhao = await _service.ObterCaminhaoyId(id);
                if (caminhao == null)
                {
                    return NotFound();
                }
                var viewModel = _mapper.Map<CaminhaoViewModel>(caminhao);

                return Ok(viewModel);
            } 
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }

        }

        [HttpPost]
        public ActionResult Post([FromBody] CaminhaoViewModel viewModel)
        {
            try
            {
                var caminhao = _mapper.Map<Caminhao>(viewModel);
                _service.CriarCaminhao(caminhao);
                return CreatedAtAction(nameof(Get), new { id = caminhao.Id }, caminhao);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CaminhaoUpdateViewModel viewModel)
        {
            try
            {
                var caminhao = _mapper.Map<Caminhao>(viewModel);
                if (id != caminhao.Id)
                {
                    return BadRequest();
                }

                var updatedCaminhao = await _service.AtualizarCaminhao(caminhao);

                if (updatedCaminhao == null)
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var caminhao = await _service.ExcluirCaminhao(id);

                if (caminhao == null)
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
    }
}
