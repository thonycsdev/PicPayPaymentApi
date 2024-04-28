using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.UseCases.Usuarios.Delete;
using Application.UseCases.Usuarios.GetAll;
using Application.UseCases.Usuarios.GetById;
using Application.UseCases.Usuarios.Update;
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
            if (result.Status != StatusCodeObjectResponse.Sucess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ObjectResponse<UsuarioResponse>>> CreateUsuario(
            UsuarioRequest input
        )
        {
            var result = await _mediator.Send(input);
            if (result.Status != StatusCodeObjectResponse.Sucess)
            {
                return BadRequest(result);
            }
            return Created("Sucess", result);
        }

        [HttpGet]
        public async Task<ActionResult<ObjectResponse<UsuarioResponse>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllUsuariosQuery());
            if (result.Status != StatusCodeObjectResponse.Sucess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<ObjectResponse<UsuarioResponse>>> DeleteUsuario(Guid id)
        {
            var result = await _mediator.Send(new DeleteUsuarioById(id));
            if (result.Status != StatusCodeObjectResponse.Sucess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<ObjectResponse<UsuarioResponse>>> UpdateUsuario(
            Guid usuario_id,
            UsuarioRequest input
        )
        {
            var result = await _mediator.Send(new UpdateUsuarioRequest());
            if (result.Status != StatusCodeObjectResponse.Sucess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
