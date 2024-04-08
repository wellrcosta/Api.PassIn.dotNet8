using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;

namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof (ResponseRegisteredEventsJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof (ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestEventJson request)
        {
            try
            {
                var useCase = new RegisterEventUseCase();
                var response = useCase.Execute(request);
                return Created(string.Empty, response);
            }
            catch (PassInException exception)
            {
                return BadRequest(new ResponseErrorJson(exception.Message));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson("An error occurred"));
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof (ResponseEventJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof (ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            try
            {
                var useCase = new GetEventByIdUseCase();

                var response = useCase.Execute(id);

                return Ok(response);
            }
            catch (PassInException e)
            {
                return NotFound(new ResponseErrorJson(e.Message));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson("An error occurred"));
            }
        }
    }
}
