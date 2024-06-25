using AutoMapper;
using Garbage.Collection.API.ViewModels;
using Garbage.Collection.Business.Service.Interfaces;
using Garbage.Collection.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Garbage.Collection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoService _service;
        private readonly IMapper _mapper;
        public AgendamentoController(IAgendamentoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgendamentoViewModel>>> Get()
        {
            try
            {
                var agendamentos = await _service.ObterAgendamentos();
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
