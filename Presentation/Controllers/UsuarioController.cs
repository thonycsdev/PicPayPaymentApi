using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.UseCases.Usuarios.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ObjectResponse<UsuarioResponse>>> GetUsuario(Guid id)
        {
            var result = await _mediator.Send(new GetUsuarioByIdQuery(id));
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<ObjectResponse<UsuarioResponse>>> CreateUsuario(UsuarioRequest input)
        {
            var result = await _mediator.Send(input);
            return Ok(result);
        }
    }
}
