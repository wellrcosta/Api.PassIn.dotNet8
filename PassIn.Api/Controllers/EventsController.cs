using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure.Validators;

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
            var validator = new RequestEventJsonValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "An error occurred";

                return BadRequest(new ResponseErrorJson(errorMessages));
}
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
